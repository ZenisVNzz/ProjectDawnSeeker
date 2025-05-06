using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.TextCore.Text;
using System.Linq;
using static UnityEngine.EventSystems.EventTrigger;

public class BattleManager : MonoBehaviour
{
    public List<CharacterInBattle> TeamPlayer = new List<CharacterInBattle>();
    public List<CharacterInBattle> TeamAI = new List<CharacterInBattle>();
    public List<PlannedAction> plannedActions = new List<PlannedAction>();

    private CharacterInBattle selectedCharacter = null;
    public BattleUI battleUI;
    public Button startTurnButton;
    public SelectSkill selectSkill;
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
            characterInBattle.OnDeath += RemoveDeathChar;
        }     
        EnablePlayerTeam(true);
        selectNum = 0;
        startTurnButton.onClick.AddListener(() =>
        {
            int actionAble = TeamPlayer.Count(c => c.isActionAble);
            if (plannedActions.Count >= actionAble)
            {
                Debug.Log("Thực thi hành động");
                Action();
            }
            else
            {
                Debug.Log("Chưa đủ hành động để thực thi");
            }    
        });
    }

    private void RemoveDeathChar(CharacterInBattle character)
    {
        TeamPlayer.Remove(character);
    }    

    public void OnCharacterClicked(CharacterInBattle character)
    {
        bool isPlayerSelectingTarget = SelectSkill.isPlayerSelectingTarget;
        if (!character.isAlive) return;

        if (isPlayerTurn)
        {
            if (TeamPlayer.Contains(character) && character.isActionAble)
            {
                selectedCharacter = character;
                battleUI.ShowSkillUI(character);
                SelectSkill.characterInBattle = character;
                Debug.Log("Đã chọn: " + character.charName);
            }
            else if (selectedCharacter != null && TeamAI.Contains(character))
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
                    SelectSkill.selectedSkill = null;
                }    
            }
        }
    }

    public void Action()
    {
        StartCoroutine(ExecuteActionsSequentially());
    }    

    void EnemyTurn()
    {
        CharacterInBattle enemy = GetFirstAlive(TeamAI);
        CharacterInBattle target = GetFirstAlive(TeamPlayer);
        if (enemy == null || target == null)
        {
            CheckWinLose();
            return;
        }
        foreach (CharacterInBattle character in TeamPlayer)
        {
            character.OnEndTurn();
        }
        foreach (CharacterInBattle character in TeamAI)
        {
            character.StartTurn();
        }   
        isPlayerTurn = false;
        if (enemy != null && target != null && enemy.isActionAble == true)
        {
            for (int i = 0; i <= 2; i++)
            {
                int skillIndex = UnityEngine.Random.Range(0, enemy.skillList.Count);
                int targetIndex = UnityEngine.Random.Range(0, TeamPlayer.Count);
                SkillBase skill = enemy.skillList[skillIndex];
                CharacterInBattle targetPlayer = TeamPlayer[targetIndex];
                PlannedAction action = new PlannedAction
                {
                    Caster = enemy,
                    Target = targetPlayer,
                    Skill = skill
                };
                plannedActions.Add(action);
            }      
            Action();
        }
        else if (enemy.isActionAble == false)
        {        
            Debug.Log($"{enemy.name} không thể hành động trong lượt này");
            OnNextTurn();
        }
        else
        {
            CheckWinLose();
        }    
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
            }
        }
        else if (NormalCurrentTurn >= NormalMaxTurn)
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

    float GetTotalHP(List<CharacterInBattle> team)
    {
        float totalHP = 0;
        foreach (var character in team)
        {
            totalHP += character.HP;
        }
        return totalHP;
    }

    private IEnumerator ExecuteActionsSequentially()
    {
        foreach (PlannedAction action in plannedActions)
        {
            if (!action.Target.isAlive)
            {
                int targetIndex = UnityEngine.Random.Range(0, TeamPlayer.Count);
                action.Target = TeamPlayer[targetIndex];
            }
            action.Skill.DoAction(action.Caster, action.Target);

            if (action.Caster.isBleeding == true)
            {
                action.Caster.TakeBleedingDamage(action.Caster.ATK * 0.15f);
            }

            while (action.Caster.currentState != State.Idle)
            {
                yield return null;
            }
            if (TeamPlayer.Count == 0 || TeamAI.Count == 0)
            {
                break;
            }
            yield return new WaitForSeconds(1f);
        }

        plannedActions.Clear();
        yield return new WaitForSeconds(1.5f);
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
        if (TeamAI.Count == 0 || TeamPlayer.Count == 0)
        {
            CheckWinLose();
        }
    }
}