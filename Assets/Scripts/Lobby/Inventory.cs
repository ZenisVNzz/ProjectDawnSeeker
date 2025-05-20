using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<CharacterData> summonedCharacters = new List<CharacterData>();

    public void AddCharacter(CharacterData character)
    {
        if (!summonedCharacters.Contains(character))
        {
            summonedCharacters.Add(character);
            Debug.Log($"Da them {character.characterName} vao Inventory");
        }
    }

    public void RemoveCharacter(CharacterData character)
    {
        if (summonedCharacters.Contains(character))
        {
            summonedCharacters.Remove(character);
            Debug.Log($"Da xoa {character.characterName} khoi Inventory");
        }
    }

}
