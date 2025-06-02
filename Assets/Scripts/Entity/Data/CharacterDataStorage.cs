using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataStorage : MonoBehaviour
{
    public List<CharacterData> characterDataList = new List<CharacterData>();

    public CharacterData GetCharacterByID(int id)
    {
        return characterDataList.Find(character => character.characterID == id);
    }
}
