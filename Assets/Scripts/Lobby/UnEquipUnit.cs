using UnityEngine;
using UnityEngine.UI;

public class UnEquipUnit : MonoBehaviour
{
    private EquipedUnit equipedUnit;
    public CharDataStorage charDataStorage;
    public GameObject equip;
    private void Start()
    {
        equipedUnit = FindAnyObjectByType<EquipedUnit>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            EquipedUnit.equipedUnit.Remove(charDataStorage.characterData);
            Inventory.Instance.currentDataSave.equipedChar.Remove(charDataStorage.characterData.characterID);
            Inventory.Instance.SaveGame();
            equipedUnit.UpdateUI();
            gameObject.SetActive(false);
            equip.SetActive(true);
        });
    }
}
