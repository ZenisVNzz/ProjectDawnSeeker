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
        BattleCharacter enemy = GetFirstAlive(TeamAI);
        BattleCharacter target = GetFirstAlive(TeamPlayer);

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
}