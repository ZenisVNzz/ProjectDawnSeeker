using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipUnit : MonoBehaviour
{
    private EquipedUnit equipedUnit;
    public CharDataStorage charDataStorage;
    public GameObject unEquip;

    private void Start()
    {      
        equipedUnit = FindAnyObjectByType<EquipedUnit>();
        Button button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            EquipedUnit.equipedUnit.Add(charDataStorage.characterData);
            equipedUnit.UpdateUI();
            gameObject.SetActive(false);
            unEquip.SetActive(true);
        });
    }  
}
