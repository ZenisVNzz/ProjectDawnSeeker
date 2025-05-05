using UnityEngine;

public class InitializeCharacter : MonoBehaviour
{
    public CharacterData playerCharacter;
    public CharacterData enemyCharacter;
    public CharacterInBattle playerCharacterInBattle;
    public CharacterInBattle enemyCharacterInBattle;

    void Awake()
    {
        playerCharacterInBattle.Initialize(playerCharacter);
        enemyCharacterInBattle.Initialize(enemyCharacter);
    }
}
