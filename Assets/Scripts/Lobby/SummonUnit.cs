using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SummonUnit : MonoBehaviour
{
    public SummonPool summonPool;
    public InventoryUI inventoryUI;
    private List<CharacterData> summonedCharacters = new List<CharacterData>();

    public void SummonCharacter()
    {
        if (summonPool == null || summonPool.CharacterPool == null || summonPool.CharacterPool.Count == 0)
        {
            Debug.LogError("SummonPool hoặc CharacterPool chưa được thiết lập hoặc rỗng!");
            return;
        }

        int randomIndex = Random.Range(100001, 100004);
        CharacterData selectedCharacter = summonPool.CharacterPool.Find(CharacterData => CharacterData.characterID == randomIndex);
        summonedCharacters.Add(selectedCharacter);
        inventoryUI.UpdateSummonedCharacters(summonedCharacters);
        Debug.Log($"Da trieu hoi {selectedCharacter.characterName} ");
    }
}