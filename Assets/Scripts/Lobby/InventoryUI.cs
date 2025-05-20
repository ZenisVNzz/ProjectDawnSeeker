using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Transform characterSlotContainer;
    public GameObject characterSlotPrefab;
    public Inventory inventory;
    public GameObject characterInfo;


    public void UpdateSummonedCharacters(List<CharacterData> summonedCharacters)
    {
        for (int i = 0; i < summonedCharacters.Count; i++)
        {
            if (i >= characterSlotContainer.childCount)
            {
                Debug.LogError("Số lượng nhân vật vượt quá số lượng Slot!");
                return;
            }

            Transform slot = characterSlotContainer.GetChild(i);
            slot.gameObject.SetActive(true); // Hiển thị Slot
            CharDataStorage charDataStorage = slot.GetComponent<CharDataStorage>();
            charDataStorage.characterData = summonedCharacters[i];
            slot.transform.Find("Mask2D/CharIMG").GetComponent<Image>().sprite = summonedCharacters[i].characterSprite;
        }

    }
    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    public void Start()
    {
        characterInfo.SetActive(false);
        inventory = FindAnyObjectByType<Inventory>();
        if (inventory != null && inventory.summonedCharacters.Count != 0)
        {
            UpdateSummonedCharacters(inventory.summonedCharacters);
        }      
    }
}
