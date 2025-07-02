using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class EquipUnit : MonoBehaviour
{
    private EquipedUnit equipedUnit;
    public CharDataStorage charDataStorage;
    public GameObject unEquip;

    private async Task Start()
    {
        await WaitForInializeData();
        equipedUnit = FindAnyObjectByType<EquipedUnit>();
        if (EquipedUnit.equipedUnit.Any(c => c.characterID == charDataStorage.characterData.characterID))
        {
            StartCoroutine(ChangeButton());
        }    
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if (EquipedUnit.equipedUnit.Count >= 3)
            {
                Debug.Log("Only 3 character can be equiped");
                return;
            }
            if (Inventory.Instance.currentDataSave.isCompletedTarvenTutorial && !Inventory.Instance.currentDataSave.isCompletedManageTutorial)
            {
                EquipTutorial equipTutorial = FindAnyObjectByType<EquipTutorial>();
                equipTutorial.OnClick();
            }
            EquipedUnit.equipedUnit.Add(charDataStorage.characterData);
            Inventory.Instance.currentDataSave.equipedChar.Add(charDataStorage.characterData.characterID);
            Inventory.Instance.SaveGame();
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

    private async Task WaitForInializeData()
    {
        while (charDataStorage == null || charDataStorage.characterData == null)
        {
            await Task.Yield();
        }
    }    
}
