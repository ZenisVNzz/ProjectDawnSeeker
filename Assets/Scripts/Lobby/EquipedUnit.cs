using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EquipedUnit : MonoBehaviour
{
    public static List<CharacterData> equipedUnit = new List<CharacterData>();
    public Transform unitContainer;
    public List<GameObject> equipedUnitSlot;

    private void Start()
    {
        List<int> equipedChar = GameManager.Instance.currentDataSave.equipedChar.ToList();
        if (equipedChar.Count > 0)
        {
            equipedUnit = new List<CharacterData>();
            Inventory.Instance.currentDataSave.equipedChar.Clear();
            foreach (var ID in equipedChar)
            {
                CharacterData charData = Inventory.Instance.summonedCharacters.Find(c => c.characterID == ID);
                equipedUnit.Add(charData);
                Inventory.Instance.currentDataSave.equipedChar.Add(charData.characterID);
                Inventory.Instance.SaveGame();
            }
            GameManager.Instance.currentDataSave.equipedChar.Clear();
        }
       
        UpdateUI();
    }

    public void UpdateUI()
    {
        foreach (GameObject equipSLot in equipedUnitSlot)
        {
            equipSLot.SetActive(false);
        }
        UpdateSummonedCharacters(equipedUnit);
    }

    public void UpdateSummonedCharacters(List<CharacterData> character)
    {
        for (int i = 0; i < character.Count; i++)
        {

            Transform slot = unitContainer.GetChild(i);
            GameObject Slot = slot.transform.Find("Mask2D/CharIMG").gameObject;
            CharDataStorage charDataStorage = slot.GetComponent<CharDataStorage>();
            charDataStorage.characterData = character[i];
            slot.transform.Find("Mask2D/CharIMG").GetComponent<Image>().sprite = character[i].characterSprite;
            Slot.SetActive(true);
        }

    }
}
