using UnityEngine;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    public List<BattleCharacter> TeamPlayer = new List<BattleCharacter>();
    public List<BattleCharacter> TeamAI = new List<BattleCharacter>();

    private BattleCharacter selectedCharacter = null;
    public bool BossLevel = false;
    private bool isPlayerTurn = true;
    private int NormalMaxTurn = 15;
    private int NormalCurrentTurn = 1;
    private int BossMaxTurn = 30;
    private int BossCurrentTurn = 1;

    void Start()
    {
        EnablePlayerTeam(true);
    }

    public void OnCharacterClicked(BattleCharacter character)
    {
        if (!character.isAlive) return;

        if (isPlayerTurn)
        {
            if (TeamPlayer.Contains(character))
            {
                selectedCharacter = character;
                Debug.Log("Đã chọn: " + character.characterName);
            }
            else if (selectedCharacter != null && TeamAI.Contains(character))
            {
                selectedCharacter.Attack(character, selectedCharacter);
                Debug.Log(selectedCharacter.characterName + " tấn công " + character.characterName);

                selectedCharacter = null;
                isPlayerTurn = false;

                Invoke("EnemyTurn", 1.5f);
            }
        }
    }

    void EnemyTurn()
    {
<<<<<<< Updated upstream
        BattleCharacter enemy = GetFirstAlive(TeamAI);
        BattleCharacter target = GetFirstAlive(TeamPlayer);
=======
        for (int i = 0; i <= 2; i++)
        {
            foreach (var enemy in TeamAI)
            {
                int skillIndex = UnityEngine.Random.Range(0, enemy.skillList.Count);
                //CharacterInBattle chosen = GetRandomAlive(TeamPlayer);
                CharacterInBattle chosen = GetLowHPTarget(TeamPlayer);
                SkillBase skill = enemy.skillList[skillIndex];
                EnemyPlannedAction action = new EnemyPlannedAction
                {
                    Caster = enemy,
                    Target = chosen,
                    Skill = skill
                };
>>>>>>> Stashed changes

        if (enemy != null && target != null)
        {
            enemy.Attack(target, enemy);
            Debug.Log(enemy.characterName + " (máy) tấn công " + target.characterName);
        }

        CheckWinLose();
        NormalTurn++;
        BossTurn++;
        isPlayerTurn = true;
        EnablePlayerTeam(true);
    }

    BattleCharacter GetFirstAlive(List<BattleCharacter> team)
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
            }
        }
        else if (NormalTurn >= NormalMaxTurn)
        {
            if(GetTotalHP(TeamPlayer) >= GetTotalHP(TeamAI))
            {
                Debug.Log("Chiến Thắng!");
            }
            else
            {
                Debug.Log("Thất Bại!");

            }
            EnablePlayerTeam(false);
        }
        if (GetFirstAlive(TeamPlayer) == null)
        {
            Debug.Log("Thất Bại!");
        }
        else if (GetFirstAlive(TeamAI) == null)
        {
            Debug.Log("Chiến Thắng!");
        }
    }

    int GetTotalHP(List<BattleCharacter> team)
    {
        int totalHP = 0;
        foreach (var character in team)
        {
            totalHP += character.HP;
        }
        return totalHP;
    }
<<<<<<< Updated upstream
=======

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

                action.Skill.DoAction(action.Caster, action.Target);

                if (TeamPlayer.All(c => !c.isAlive) || TeamAI.All(c => !c.isAlive))
                {
                    break;
                }

                while (action.Caster.currentState != State.Idle)
                {
                    yield return null;
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

                action.Skill.DoAction(action.Caster, action.Target);

                if (TeamPlayer.All(c => !c.isAlive) || TeamAI.All(c => !c.isAlive))
                {
                    break;
                }

                while (action.Caster.currentState != State.Idle)
                {
                    yield return null;
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










    public CharacterInBattle GetLowHPTarget(List<CharacterInBattle> list)
    {
        var aliveList = list.Where(x => x.isAlive).ToList();
        if (aliveList.Count == 0) return null;
        // Lấy nhân vật có HP thấp nhất
        var lowestHPCharacter = aliveList.OrderBy(x => x.HP).FirstOrDefault();
        return lowestHPCharacter;
    }
>>>>>>> Stashed changes
}