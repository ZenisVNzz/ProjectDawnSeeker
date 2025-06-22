using System.Collections;
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
        button.onClick.AddListener(() =>
        {
            if (Inventory.Instance.currentDataSave.isCompletedTarvenTutorial && !Inventory.Instance.currentDataSave.isCompletedManageTutorial)
            {
                EquipTutorial equipTutorial = FindAnyObjectByType<EquipTutorial>();
                equipTutorial.OnClick();
            }
            EquipedUnit.equipedUnit.Add(charDataStorage.characterData);
            equipedUnit.UpdateUI();
            StartCoroutine(ChangeButton());
        });
    }  

    IEnumerator ChangeButton()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
        unEquip.SetActive(true);
    }    
}
