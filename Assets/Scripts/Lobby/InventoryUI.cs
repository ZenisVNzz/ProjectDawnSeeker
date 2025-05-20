using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Transform characterSlotContainer;
    public GameObject characterSlotPrefab;

    public void UpdateSummonedCharacters(List<CharacterData> summonedCharacters)
    {
        // Xóa danh sách cũ
        foreach (Transform slot in characterSlotContainer)
        {
            Destroy(slot.gameObject);
        }

        // Hiển thị nhân vật đã triệu hồi
        foreach (CharacterData character in summonedCharacters)
        {
            GameObject newSlot = Instantiate(characterSlotPrefab, characterSlotContainer);
            newSlot.transform.GetChild(0).GetComponent<Image>().sprite = character.characterSprite;
        }
    }
        public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

}
