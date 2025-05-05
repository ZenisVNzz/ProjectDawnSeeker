using System.Collections.Generic;
using UnityEngine;

public class InitializeCharacter : MonoBehaviour
{
    public CharacterData playerCharacter;
    public CharacterData enemyCharacter;
    public List<CharacterInBattle> playerCharacterInBattle;
    public CharacterInBattle enemyCharacterInBattle;

    void Awake()
    {
        foreach (CharacterInBattle character in playerCharacterInBattle)
        {
            character.Initialize(playerCharacter);
        }
        enemyCharacterInBattle.Initialize(enemyCharacter);
    }
}
