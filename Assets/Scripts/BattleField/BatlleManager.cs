using UnityEngine;
using System.Collections.Generic;
using System;

public class BattleManager : MonoBehaviour
{
    public List<CharacterInBattle> TeamPlayer = new List<CharacterInBattle>();
    public List<CharacterInBattle> TeamAI = new List<CharacterInBattle>();

    private CharacterInBattle selectedCharacter = null;
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

    public void OnCharacterClicked(CharacterInBattle character)
    {
        if (!character.isAlive) return;

        if (isPlayerTurn)
        {
            if (TeamPlayer.Contains(character))
            {
                selectedCharacter = character;
                Debug.Log("Đã chọn: " + character.charName);
            }
            else if (selectedCharacter != null && TeamAI.Contains(character))
            {
                selectedCharacter.Attack(character, selectedCharacter);
                Debug.Log(selectedCharacter.charName + " tấn công " + character.charName);

                selectedCharacter = null;
                isPlayerTurn = false;

                Invoke("EnemyTurn", 1.5f);
            }
        }
    }

    void EnemyTurn()
    {
        CharacterInBattle enemy = GetFirstAlive(TeamAI);
        CharacterInBattle target = GetFirstAlive(TeamPlayer);

        if (enemy != null && target != null)
        {
            enemy.Attack(target, enemy);
            Debug.Log(enemy.charName + " (máy) tấn công " + target.charName);
        }

        CheckWinLose();
        NormalCurrentTurn++;
        BossCurrentTurn++;
        isPlayerTurn = true;
        EnablePlayerTeam(true);
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
}