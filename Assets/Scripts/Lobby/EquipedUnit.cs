using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipedUnit : MonoBehaviour
{
    public static List<CharacterData> equipedUnit = new List<CharacterData>();
    public Transform unitContainer;
    public List<GameObject> equipedUnitSlot;

    private void Start()
    {
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
