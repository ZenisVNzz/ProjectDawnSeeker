using System.Collections.Generic;
using UnityEngine;

public class InitializeCharacter : MonoBehaviour
{
    public List<CharacterData> playerCharacter;
    public List<CharacterData> enemyCharacter;
    public List<CharacterInBattle> playerCharacterInBattle;
    public List<CharacterInBattle> enemyCharacterInBattle;
    public BattleManager battleManager;
    public BattleUI battleUI;

    private List<CharacterInBattle> nullCharacter = new List<CharacterInBattle>();

    void Awake()
    {
        //playerCharacter = EquipedUnit.equipedUnit;
        int playerDataCount = playerCharacter.Count;
        int CharacterInBattleCount = playerCharacterInBattle.Count;
        if (CharacterInBattleCount > playerDataCount)
        {
            for (int i = playerDataCount; i < CharacterInBattleCount; i++)
            {
                battleManager.TeamPlayer.Remove(playerCharacterInBattle[i]);
                battleUI.activeCharacter.Remove(playerCharacterInBattle[i]);
                nullCharacter.Add(playerCharacterInBattle[i]);
            }
            foreach (CharacterInBattle character in nullCharacter)
            {
                playerCharacterInBattle.Remove(character);
                Destroy(character.gameObject);
            }
        }
        for (int i = 0; i < playerCharacterInBattle.Count; i++)
        {
            playerCharacterInBattle[i].Initialize(playerCharacter[i]);
        }
        for (int i = 0; i < enemyCharacterInBattle.Count; i++)
        {
            enemyCharacterInBattle[i].Initialize(enemyCharacter[i]);
        }
    }
}
