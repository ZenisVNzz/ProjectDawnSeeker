using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public List<CharacterInBattle> TeamPlayer = new List<CharacterInBattle>();
    public List<CharacterInBattle> TeamAI = new List<CharacterInBattle>();
    public List<PlannedAction> plannedActions = new List<PlannedAction>();
    public List<EnemyPlannedAction> enemyPlannedAction = new List<EnemyPlannedAction>();

    [SerializeField] CharacterInBattle selectedCharacter = null;
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
        foreach (CharacterInBattle characterInBattle in TeamPlayer)
        {
            characterInBattle.OnDeath += OnDeath;
        }     
        EnablePlayerTeam(true);
        selectNum = 0;
        startTurnButton.onClick.AddListener(() =>
        {
            int actionAble = TeamPlayer.Count(c => c.isActionAble);
            if (plannedActions.Count >= actionAble)
            {
                Debug.Log("Thực thi hành động");
                selectSkill.DisableSkillUI();
                Action();
                startTurnButton.interactable = false;
            }
            else
            {
                Debug.Log("Chưa đủ hành động để thực thi");
            }    
        });
        InitializedEnemyAttack();
    }

    private void OnDeath(CharacterInBattle character)
    {
    }    

    public void OnCharacterClicked(CharacterInBattle character)
    {
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

    void InitializedEnemyAttack()
    {
        for (int i = 0; i <= 2; i++)
        {
            foreach (var enemy in TeamAI)
            {
                int skillIndex = UnityEngine.Random.Range(0, enemy.skillList.Count);
                CharacterInBattle chosen = GetRandomAlive(TeamPlayer);
                SkillBase skill = enemy.skillList[skillIndex];
                EnemyPlannedAction action = new EnemyPlannedAction
                {
                    Caster = enemy,
                    Target = chosen,
                    Skill = skill
                };

                enemyPlannedAction.Add(action);
                StartCoroutine(AddEnemyAction(enemy, chosen, skill));
            }
            
        }
    }

    public CharacterInBattle GetRandomAlive(List<CharacterInBattle> list)
    {
        var aliveList = list.Where(x => x.isAlive).ToList();
        if (aliveList.Count == 0) return null;

        int index = Random.Range(0, aliveList.Count);
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
            }
        }
        else if (NormalCurrentTurn >= NormalMaxTurn)
        {
            if(GetTotalHP(TeamPlayer) >= GetTotalHP(TeamAI))
            {
                Debug.Log("Chiến Thắng!");
                resultUI.ShowVictoryUI();
                StopAllCoroutines();
            }
            else
            {
                Debug.Log("Thất Bại!");
                resultUI.ShowFailedUI();
                StopAllCoroutines();
            }
            EnablePlayerTeam(false);
        }
        if (GetFirstAlive(TeamPlayer) == null)
        {
            Debug.Log("Thất Bại!");
            resultUI.ShowFailedUI();
            StopAllCoroutines();
        }
        else if (GetFirstAlive(TeamAI) == null)
        {
            Debug.Log("Chiến Thắng!");
            resultUI.ShowVictoryUI();
            StopAllCoroutines();
        }
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
        if (PlayerTurn)
        {
            foreach (PlannedAction action in plannedActions)
            {
                if (!action.Target.isAlive)
                {
                    actionOrderUI.RemoveAction(action.Caster, action.Skill);
                    continue;
                }

                Vector3 originalPos = action.Caster.transform.position;
                if (action.Skill.move)
                {
                    yield return StartCoroutine(MoveToTarget(action.Caster, action.Target));
                }             
                
                action.Skill.DoAction(action.Caster, action.Target);

                if (TeamPlayer.All(c => !c.isAlive) || TeamAI.All(c => !c.isAlive))
                {
                    break;
                }

                while (action.Caster.currentState != State.Idle)
                {
                    yield return null;
                }

                if (action.Skill.move)
                {
                    yield return StartCoroutine(ReturnToOriginalPosition(action.Caster, originalPos));
                }
              
                actionOrderUI.RemoveAction(action.Caster, action.Skill);

                if (action.Caster.isBleeding == true)
                {
                    action.Caster.TakeBleedingDamage(action.Caster.ATK * 0.15f);
                }
                if (action.Caster.isAlive == false)
                {
                    break;
                }
                yield return new WaitForSeconds(1f);
            }
        }
        else
        {
            foreach (EnemyPlannedAction action in enemyPlannedAction)
            {
                if (!action.Caster.isActionAble)
                {
                    actionOrderUI.RemoveAction(action.Caster, action.Skill);
                    continue;
                }    
                if (!action.Target.isAlive)
                {
                    actionOrderUI.RemoveAction(action.Caster, action.Skill);
                    continue;
                }

                Vector3 originalPos = action.Caster.transform.position;
                if (action.Skill.move)
                {
                    yield return StartCoroutine(MoveToTarget(action.Caster, action.Target));
                }

                action.Skill.DoAction(action.Caster, action.Target);

                if (TeamPlayer.All(c => !c.isAlive) || TeamAI.All(c => !c.isAlive))
                {
                    break;
                }

                while (action.Caster.currentState != State.Idle)
                {
                    yield return null;
                }

                if (action.Skill.move)
                {
                    yield return StartCoroutine(ReturnToOriginalPosition(action.Caster, originalPos));
                }

                actionOrderUI.RemoveAction(action.Caster, action.Skill);

                if (action.Caster.isBleeding == true)
                {
                    action.Caster.TakeBleedingDamage(action.Caster.ATK * 0.15f);
                }
                if (action.Caster.isAlive == false)
                {
                    actionOrderUI.RemoveAction(action.Caster, action.Skill);
                    break;
                }
                yield return new WaitForSeconds(1f);
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

    public void OnNextTurn()
    {
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
                InitializedEnemyAttack();
            }
            startTurnButton.interactable = true;
            selectSkill.EnableSkillUI();
        }
        else
        {
            if (enemyPlannedAction.Count <= 0)
            {
                InitializedEnemyAttack();
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
        attacker.PlayEffectOnEndAction();
    }

}