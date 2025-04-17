using UnityEngine;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    public List<BattleCharacter> TeamPlayer = new List<BattleCharacter>();
    public List<BattleCharacter> TeamAI = new List<BattleCharacter>();

    private BattleCharacter selectedCharacter = null;
    private bool isPlayerTurn = true;

    public bool isAutoBattle = false;

    public bool isAuto = false;

    public void ToggleAutoMode()
    {
        isAuto = !isAuto;
        Debug.Log("Chế độ Auto: " + isAuto);
    }


    void Start()
    {
        EnablePlayerTeam(true);

        if (isAutoBattle)
        {
            Invoke("AutoPlayerTurn", 1f);
        }
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
        isPlayerTurn = true;
        if (isAutoBattle)
        {
            AutoPlayerTurn(); //Invoke("AutoPlayerTurn", 1.5f); // gọi lại auto sau khi máy đánh
        }
        else
        {
            EnablePlayerTeam(true);
        }
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

        CheckWinLose();
    }

    void CheckWinLose()
    {
        if (GetFirstAlive(TeamPlayer) == null)
        {
            Debug.Log("Thất Bại!");
        }
        else if (GetFirstAlive(TeamAI) == null)
        {
            Debug.Log("Chiến Thắng!");
        }
    }

    void AutoPlayerTurn()
    {
        if (!isPlayerTurn) return;

        BattleCharacter playerChar = GetFirstAlive(TeamPlayer);
        BattleCharacter target = GetFirstAlive(TeamAI);

        if (playerChar != null && target != null)
        {
            playerChar.Attack(target, playerChar);
            Debug.Log(playerChar.characterName + " (auto) tấn công " + target.characterName);
        }

        isPlayerTurn = false;
        Invoke("EnemyTurn", 1.5f);
    }
}