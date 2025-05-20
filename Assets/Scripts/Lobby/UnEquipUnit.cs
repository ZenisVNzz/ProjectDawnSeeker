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
            equipedUnit.UpdateUI();
            gameObject.SetActive(false);
            equip.SetActive(true);
        });
    }
}
