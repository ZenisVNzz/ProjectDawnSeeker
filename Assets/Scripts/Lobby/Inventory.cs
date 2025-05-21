using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
