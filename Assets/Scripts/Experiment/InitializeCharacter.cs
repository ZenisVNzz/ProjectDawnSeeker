using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeCharacter : MonoBehaviour
{
    public List<CharacterData> playerCharacter = new List<CharacterData>();
    public List<CharacterData> enemyCharacter = new List<CharacterData>();
    public List<CharacterInBattle> playerCharacterInBattle = new List<CharacterInBattle>();
    public List<CharacterInBattle> enemyCharacterInBattle = new List<CharacterInBattle>();
    public BattleManager battleManager;
    public BattleUI battleUI;
    public CharacterData defaultChar;

    private List<CharacterInBattle> nullCharacter = new List<CharacterInBattle>();

    void Awake()
    {
        //playerCharacter = new List<CharacterData>();
        //enemyCharacter = new List<CharacterData>();

        //if (SceneManager.GetActiveScene().name != "Battle")
        //{       
        //    playerCharacter.Add(defaultChar);
        //}
        //else
        //{
        //    playerCharacter = EquipedUnit.equipedUnit;
        //}
        //GameManager gameManager = FindAnyObjectByType<GameManager>();
        //StageData stageData = gameManager.transform.Find("StageData").GetComponent<StageData>();
        //foreach (Enemy enemy in stageData.enemies)
        //{
        //    enemyCharacter.Add(enemy.characterData);
        //}
        int playerDataCount = playerCharacter.Count;
        int enemyDataCount = enemyCharacter.Count;
        int CharacterInBattleCount = playerCharacterInBattle.Count;
        int EnemyInBattleCount = enemyCharacterInBattle.Count;
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
        if (EnemyInBattleCount > enemyDataCount)
        {
            for (int i = enemyDataCount; i < EnemyInBattleCount; i++)
            {
                battleManager.TeamAI.Remove(enemyCharacterInBattle[i]);
                battleUI.activeEnemyCharacter.Remove(enemyCharacterInBattle[i]);
                nullCharacter.Add(enemyCharacterInBattle[i]);
            }
            foreach (CharacterInBattle character in nullCharacter)
            {
                enemyCharacterInBattle.Remove(character);
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
