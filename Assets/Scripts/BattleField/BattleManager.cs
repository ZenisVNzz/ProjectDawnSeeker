using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Behavior;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public List<CharacterInBattle> TeamPlayer = new List<CharacterInBattle>();
    public List<CharacterInBattle> TeamAI = new List<CharacterInBattle>();
    public List<PlannedAction> plannedActions = new List<PlannedAction>();
    public List<EnemyPlannedAction> enemyPlannedAction = new List<EnemyPlannedAction>();

    public event System.Action OnEndTurn;
    public event Action<bool> OnFinishedStage;

    public List<BehaviorGraphAgent> behaviorGraphAgent;

    private CharacterInBattle selectedCharacter = null;
    public BattleUI battleUI;
    public Button startTurnButton;
    public SelectSkill selectSkill;
    public TargetArrowManager targetArrowManager;
    public ResultUI resultUI;
    public ActionOrder actionOrderUI;
    public int selectNum;
    public bool BossLevel = false;
    private bool isPlayerTurn = true;
    private int NormalMaxTurn = 15;
    private int NormalCurrentTurn = 1;
    private int BossMaxTurn = 30;
    private int BossCurrentTurn = 1;


    void Start()
    {
        behaviorGraphAgent = TeamAI.ConvertAll(c => c.GetComponent<BehaviorGraphAgent>());
        foreach (BehaviorGraphAgent agent in behaviorGraphAgent)
        {
            List<GameObject> playerObj = new List<GameObject>();
            playerObj = TeamPlayer.ConvertAll(character => character.gameObject);
            List<GameObject> enemyObj = new List<GameObject>();
            enemyObj = TeamAI.ConvertAll(character => character.gameObject);

            agent.BlackboardReference.SetVariableValue("PlayerTeam", playerObj);
            agent.BlackboardReference.SetVariableValue("MyTeam", enemyObj);
        }

        foreach (CharacterInBattle characterInBattle in TeamPlayer)
        {
            characterInBattle.OnDeath += OnDeath;
        }     
        EnablePlayerTeam(true);
        selectNum = 0;
        startTurnButton.onClick.AddListener(() =>
        {
            GameObject buttonPanel = GameObject.Find("ButtonPanel");
            Animator buttonPanelAnimator = buttonPanel.GetComponent<Animator>();
            int actionAble = TeamPlayer.Count(c => c.isActionAble);
            Debug.Log("Thực thi hành động");
            Action();
            startTurnButton.interactable = false;
            if (buttonPanelAnimator.GetCurrentAnimatorStateInfo(0).IsName("Popup"))
            {
                buttonPanelAnimator.Play("Hide");
            }
        });
        StartCoroutine(InitializEnemyAction());
    }

    private void OnDeath(CharacterInBattle character)
    {
    }    

    public void OnCharacterClicked(CharacterInBattle character)
    {
        GameObject buttonPanel = GameObject.Find("ButtonPanel");
        Animator buttonPanelAnimator = buttonPanel.GetComponent<Animator>();
        bool isPlayerSelectingTarget = SelectSkill.isPlayerSelectingTarget;
        if (!character.isAlive) return;

        if (isPlayerTurn)
        {
            if (TeamPlayer.Contains(character) && character.isActionAble && isPlayerSelectingTarget == false)
            {
                selectedCharacter = character;
                battleUI.ShowSkillUI(character);
                SelectSkill.characterInBattle = character;
                Debug.Log("Đã chọn: " + character.charName);
                battleUI.SelectingCharacter(character);
                buttonPanelAnimator.Play("Popup");
            }
            else if (SelectSkill.selectedSkill != null)
            {
                if (selectedCharacter != null && TeamAI.Contains(character) && selectSkill.GetSkillBase().passiveSkill == false && selectSkill.GetSkillBase().supportSkill == false)
                {
                    if (isPlayerSelectingTarget == true)
                    {
                        PlannedAction action = new PlannedAction
                        {
                            Caster = selectedCharacter,
                            Target = character,
                            Skill = selectSkill.GetSkillBase()
                        };
                        if (!plannedActions.Any(a => a.Caster == selectedCharacter))
                        {
                            plannedActions.Add(action);
                        }
                        else
                        {
                            plannedActions.RemoveAll(a => a.Caster == selectedCharacter);
                            plannedActions.Add(action);
                        }

                        Debug.Log($"Skill: {selectSkill.GetSkillBase().name} nhắm vào {character}");
                        SelectSkill.isPlayerSelectingTarget = false;
                        actionOrderUI.AddAction(selectedCharacter, character, selectSkill.GetSkillBase(), false);
                        SelectSkill.selectedSkill = null;    
                        buttonPanelAnimator.Play("Hide");
                    }
                }
                else if (TeamPlayer.Contains(character) && selectSkill.GetSkillBase().supportSkill && isPlayerSelectingTarget == true)
                {
                    PlannedAction action = new PlannedAction
                    {
                        Caster = selectedCharacter,
                        Target = character,
                        Skill = selectSkill.GetSkillBase()
                    };
                    if (!plannedActions.Any(a => a.Caster == selectedCharacter))
                    {
                        plannedActions.Add(action);
                    }
                    else
                    {
                        plannedActions.RemoveAll(a => a.Caster == selectedCharacter);
                        plannedActions.Add(action);
                    }
                    SelectSkill.isPlayerSelectingTarget = false;
                    actionOrderUI.AddAction(selectedCharacter, character, selectSkill.GetSkillBase(), true);
                    SelectSkill.selectedSkill = null;
                    buttonPanelAnimator.Play("Hide");
                }
            }                     
        }
    }

    public void Action()
    {
        StartCoroutine(ExecuteActionsSequentially(true));
    }    

    void EnemyTurn()
    {          
        foreach (CharacterInBattle character in TeamPlayer)
        {
            character.OnEndTurn();
        }
        foreach (CharacterInBattle character in TeamAI)
        {
            character.StartTurn();
        }   
        isPlayerTurn = false;
        StartCoroutine(ExecuteActionsSequentially(false));
    }

    IEnumerator InitializEnemyAction()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (var enemyAI in behaviorGraphAgent)
        {
            enemyAI.BlackboardReference.SetVariableValue("IsBeginTurn", true);
            yield return new WaitForSeconds(0.65f);
            enemyAI.Restart();
        }    
    }    

    public CharacterInBattle GetRandomAlive(List<CharacterInBattle> list)
    {
        var aliveList = list.Where(x => x.isAlive).ToList();
        if (aliveList.Count == 0) return null;

        int index = UnityEngine.Random.Range(0, aliveList.Count);
        return aliveList[index];
    }

    public IEnumerator AddEnemyAction(CharacterInBattle enemy, CharacterInBattle target, SkillBase skill)
    {
        if (skill.supportSkill == true)
        {
            actionOrderUI.AddAction(enemy, target, skill, true);
        }
        else
        {
            actionOrderUI.AddAction(enemy, target, skill, false);
        }
        yield return new WaitForSeconds(0.5f);
    }

    CharacterInBattle GetFirstAlive(List<CharacterInBattle> team)
    {
        foreach (var c in team)
        {
            if (c.isAlive)
                return c;
        }
        return null;
    }

    void EnablePlayerTeam(bool enable)
    {
        foreach (var character in TeamPlayer)
        {
            character.SetClickable(enable);
        }

        foreach (var character in TeamAI)
        {
            character.SetClickable(enable); // Có thể chỉnh thành false nếu không muốn click địch luôn
        }
    }

    void CheckWinLose()
    {
        if (BossLevel == true)
        {
            if (BossCurrentTurn >= BossMaxTurn)
            {
                Debug.Log("Thất Bại!");
                EnablePlayerTeam(false);
                resultUI.ShowFailedUI();
                StopAllCoroutines();
                OnFinishedStage?.Invoke(false);
            }
        }
        else if (NormalCurrentTurn >= NormalMaxTurn)
        {
            if(GetTotalHP(TeamPlayer) >= GetTotalHP(TeamAI))
            {
                Debug.Log("Chiến Thắng!");
                OnEndBattle(true);
            }
            else
            {
                Debug.Log("Thất Bại!");
                OnEndBattle(false);
            }
            EnablePlayerTeam(false);
        }
        if (GetFirstAlive(TeamPlayer) == null)
        {
            Debug.Log("Thất Bại!");
            OnEndBattle(false);
        }
        else if (GetFirstAlive(TeamAI) == null)
        {
            Debug.Log("Chiến Thắng!");
            OnEndBattle(true);
        }
    }

    public void OnEndBattle(bool victory)
    {
        ToggleX2 toggleX2 = FindFirstObjectByType<ToggleX2>();
        toggleX2.isOn = false;
        Time.timeScale = 1f;
        if (victory)
        {
            resultUI.ShowVictoryUI();
            OnFinishedStage?.Invoke(true);
        }
        else
        {
            resultUI.ShowFailedUI();
            OnFinishedStage?.Invoke(false);
        }
        StopAllCoroutines();
    }

    float GetTotalHP(List<CharacterInBattle> team)
    {
        float totalHP = 0;
        foreach (var character in team)
        {
            totalHP += character.HP;
        }
        return totalHP;
    }

    private IEnumerator ExecuteActionsSequentially(bool PlayerTurn)
    {
        ToggleX2 toggleX2 = FindFirstObjectByType<ToggleX2>();
        if (toggleX2.isOn)
        {
            Time.timeScale = 2f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        if (PlayerTurn)
        {
            EnablePlayerTeam(false);

            foreach (PlannedAction action in plannedActions)
            {
                if (!action.Target.isAlive)
                {
                    actionOrderUI.RemoveAction(action.Caster, action.Skill);
                    continue;
                }
                if (!action.Caster.isAlive)
                {
                    actionOrderUI.RemoveAction(action.Caster, action.Skill);
                    continue;
                }

                Vector3 originalPos = action.Caster.transform.position;
                if (action.Skill.move && !action.Skill.isWaitForCharge)
                {
                    yield return StartCoroutine(MoveToTarget(action.Caster, action.Target));
                }
                action.Caster.EndAllWalkSound();

                action.Skill.DoAction(action.Caster, action.Target);
                battleUI.RefreshBattleUI();

                if (action.Target.isParry)
                {
                    yield return StartCoroutine(WaitForParry(action.Caster, action.Target));
                }    

                if (!action.Caster.isAlive)
                {
                    actionOrderUI.RemoveAction(action.Caster, action.Skill);
                    yield return new WaitForSeconds(1f);
                    continue;
                }    

                if (TeamPlayer.All(c => !c.isAlive) || TeamAI.All(c => !c.isAlive))
                {
                    break;
                }

                while (action.Caster.currentState != State.Idle)
                {
                    yield return null;
                }

                action.Skill.ApplyEffectOnFinishedAttack(action.Caster, action.Target);

                if (action.Skill.move && !action.Skill.isWaitForCharge)
                {
                    yield return StartCoroutine(ReturnToOriginalPosition(action.Caster, originalPos));
                }
                action.Caster.EndAllWalkSound();

                actionOrderUI.RemoveAction(action.Caster, action.Skill);

                yield return new WaitForSeconds(0.5f);

                action.Skill.ApplyEffectOnEnd(action.Caster, action.Target);
                action.Caster.ResetState();
                action.Target.ResetState();

                if (action.Caster.isBleeding == true)
                {
                    action.Caster.TakeBleedingDamage(action.Caster.ATK * 0.15f);
                }
                if (action.Caster.isAlive == false)
                {
                    continue;
                }

                yield return new WaitForSeconds(0.5f);
            }
        }
        else
        {
            //foreach (EnemyPlannedAction action in enemyPlannedAction)
            //{
            //    if (!action.Target.isAlive)
            //    {
            //        actionOrderUI.RemoveAction(action.Caster, action.Skill);
            //        continue;
            //    }
            //    if (!action.Caster.isAlive)
            //    {
            //        actionOrderUI.RemoveAction(action.Caster, action.Skill);
            //        continue;
            //    }

            //    Vector3 originalPos = action.Caster.transform.position;

            //    if (!action.Caster.isActionAble)
            //    {
            //        if (action.Caster.isCharge)
            //        {
            //            bool canUseSkill = action.Skill.CheckSkillCondition(action.Caster, action.Target);
            //            if (canUseSkill)
            //            {
            //                if (action.Skill.GetChargeTurn() <= action.Caster.chargeTurn + 1)
            //                {
            //                    if (action.Skill.move)
            //                    {
            //                        yield return StartCoroutine(MoveToTarget(action.Caster, action.Target));
            //                    }
            //                    if (TeamPlayer.Any(c => c.isAggroUp && c.isAlive))
            //                    {
            //                        action.Target = TeamPlayer.FirstOrDefault(c => c.isAggroUp && c.isAlive);
            //                    }

            //                    action.Skill.DoSpecialAction(action.Caster, action.Target);
            //                    battleUI.RefreshBattleUI();

            //                    if (action.Target.isParry)
            //                    {
            //                        yield return StartCoroutine(WaitForParry(action.Caster, action.Target));
            //                    }

            //                    if (!action.Caster.isAlive)
            //                    {
            //                        actionOrderUI.RemoveAction(action.Caster, action.Skill);
            //                        yield return new WaitForSeconds(1f);
            //                        continue;
            //                    }

            //                    while (action.Caster.currentState != State.Idle)
            //                    {
            //                        yield return null;
            //                    }

            //                    action.Skill.ApplyEffectOnFinishedAttack(action.Caster, action.Target);

            //                    if (action.Skill.move)
            //                    {
            //                        yield return StartCoroutine(ReturnToOriginalPosition(action.Caster, originalPos));
            //                    }
            //                    action.Caster.isCharge = false;
            //                    action.Caster.isActionAble = true;

            //                    yield return new WaitForSeconds(0.5f);

            //                    action.Skill.ApplyEffectOnEnd(action.Caster, action.Target);
            //                    action.Caster.ResetState();
            //                    action.Target.ResetState();

            //                    if (action.Caster.isBleeding == true)
            //                    {
            //                        action.Caster.TakeBleedingDamage(action.Caster.ATK * 0.15f);
            //                    }

            //                    yield return new WaitForSeconds(0.5f);
            //                }  
            //                else
            //                {
            //                    action.Caster.chargeTurn++;
            //                }    
            //            }
            //            else
            //            {
            //                action.Skill.OnFailCharge(action.Caster, action.Target);
            //                action.Caster.isCharge = false;
            //            }    
            //        }
            //        else
            //        {
            //            action.Caster.OnAttackEnd();
            //        }
            //        actionOrderUI.RemoveAction(action.Caster, action.Skill);
            //        continue;
            //    }                
            foreach (var enemyAI in behaviorGraphAgent)
            {
                enemyAI.BlackboardReference.SetVariableValue("IsActionTurn", true);

                yield return StartCoroutine(WaitForEnemyActionCompletion(enemyAI));
            }
        }    
        
        if (isPlayerTurn)
        {
            plannedActions.Clear();
        }
        else
        {
            enemyPlannedAction.Clear();
        }    
        
        yield return new WaitForSeconds(1f);
        CheckWinLose();
        yield return new WaitForSeconds(0.5f);

        if (isPlayerTurn)
        {
            EnemyTurn();
        }
        else
        {
            OnNextTurn();
        }
    }

    IEnumerator WaitForEnemyActionCompletion(BehaviorGraphAgent enemy)
    {
        bool isActionTurn;
        while (enemy.BlackboardReference.GetVariableValue<bool>("OnFinishAction", out isActionTurn) && isActionTurn == false)
        {
            yield return null;
        }

        yield return new WaitForSeconds(3f);
        CheckWinLose();
        enemy.BlackboardReference.SetVariableValue("IsActionTurn", false);
        enemy.BlackboardReference.SetVariableValue("OnFinishAction", false);
    }

    public void OnNextTurn()
    {
        ToggleX2 toggleX2 = FindFirstObjectByType<ToggleX2>();
        if (toggleX2.isOn)
        {
            Time.timeScale = 1f;
        }

        NormalCurrentTurn++;
        BossCurrentTurn++;
        isPlayerTurn = true;
        battleUI.RefreshTurnUI(NormalCurrentTurn);
        EnablePlayerTeam(true);
        foreach (CharacterInBattle character in TeamAI)
        {
            character.OnEndTurn();
        }
        foreach (CharacterInBattle character in TeamPlayer)
        {
            character.StartTurn();
        }
        StartCoroutine(CheckIfPlayerCanAction(1));
        OnEndTurn?.Invoke();
    }

    public IEnumerator CheckIfPlayerCanAction(int sec)
    {
        yield return new WaitForSeconds(sec);
        if (TeamAI.All(a => !a.isAlive) || TeamPlayer.All(a => !a.isAlive))
        {
            CheckWinLose();
            yield break;
        }
        else if (TeamPlayer.Any(p => p.isActionAble))
        {
            if (enemyPlannedAction.Count <= 0)
            {
                StartCoroutine(InitializEnemyAction());
            }
            startTurnButton.interactable = true;
            selectSkill.EnableSkillUI();
        }
        else
        {
            if (enemyPlannedAction.Count <= 0)
            {
                StartCoroutine(InitializEnemyAction());
            }
            yield return new WaitForSeconds(2);           
            EnemyTurn();
        }
    }

    public IEnumerator MoveToTarget(CharacterInBattle attacker, CharacterInBattle target, float speed = 11f)
    {
        Vector3 startPos = attacker.transform.position;
        Vector3 targetPos;

        Animator animator = attacker.transform.GetComponent<Animator>();

        if (attacker.characterType == characterType.Player)
        {
            targetPos = target.transform.position + new Vector3(-1.8f, 0, 0);
        }
        else
        {
            targetPos = target.transform.position + new Vector3(1.8f, 0, 0);
        }
        
        float Timer = 0f;
        float Distance = Vector3.Distance(startPos, targetPos);

        animator.Play("Move");

        while (Timer < Distance / speed)
        {
            attacker.transform.position = Vector3.Lerp(startPos, targetPos, (Timer * speed) / Distance);
            Timer += Time.deltaTime;
            yield return null;
        }

        attacker.transform.position = targetPos;
    }

    public IEnumerator ReturnToOriginalPosition(CharacterInBattle attacker, Vector3 originalPos, float speed = 11f)
    {
        Vector3 startPos = attacker.transform.position;
        float Timer = 0f;
        float Distance = Vector3.Distance(startPos, originalPos);

        Animator animator = attacker.transform.GetComponent<Animator>();
        animator.Play("Move");

        while (Timer < Distance / speed)
        {
            attacker.transform.position = Vector3.Lerp(startPos, originalPos, (Timer * speed) / Distance);
            Timer += Time.deltaTime;
            yield return null;
        }

        attacker.transform.position = originalPos;
        animator.Play("Idle");
        attacker.OnAttackEnd();
    }

    public IEnumerator WaitForParry(CharacterInBattle attacker, CharacterInBattle target)
    {
        while (attacker.currentState != State.Idle)
        {
            yield return null;
        }

        target.Attack(target, attacker);

        while (target.currentState != State.Idle)
        {
            yield return null;
        }
    }    

    public int GetCurrentTurn()
    {
        if (BossLevel)
        {
            return BossCurrentTurn;
        }
        else
        {
            return NormalCurrentTurn;
        }
    }
}