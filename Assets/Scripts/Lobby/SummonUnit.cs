using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SummonUnit : MonoBehaviour
{
    public SummonPool summonPool;
    private List<CharacterData> summonedCharacters = new List<CharacterData>();
    public Inventory inventory;
    public void SummonCharacter()
    {
        if (summonPool == null || summonPool.CharacterPool == null || summonPool.CharacterPool.Count == 0)
        {
            Debug.LogError("SummonPool hoặc CharacterPool chưa được thiết lập hoặc rỗng!");
            return;
        }

        int randomIndex = Random.Range(100001, 100005);
        CharacterData selectedCharacter = summonPool.CharacterPool.Find(CharacterData => CharacterData.characterID == randomIndex);
        summonedCharacters.Add(selectedCharacter);
            
        if (inventory == null)
            {
                Debug.LogError("Inventory chưa được thiết lập!");
                return;
            }
            inventory.AddCharacter(selectedCharacter);

        Debug.Log($"Da trieu hoi {selectedCharacter.characterName} ");
    }
}