using System.Collections.Generic;
using UnityEngine;

public class InitializeCharacter : MonoBehaviour
{
    public List<CharacterData> playerCharacter;
    public CharacterData enemyCharacter;
    public List<CharacterInBattle> playerCharacterInBattle;
    public CharacterInBattle enemyCharacterInBattle;

    void Awake()
    {
        for (int i = 0; i < playerCharacterInBattle.Count; i++)
        {
            playerCharacterInBattle[i].Initialize(playerCharacter[i]);
        }    
        enemyCharacterInBattle.Initialize(enemyCharacter);
    }
}
