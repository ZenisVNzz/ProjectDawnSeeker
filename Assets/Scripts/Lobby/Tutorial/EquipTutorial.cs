using UnityEngine;
using UnityEngine.UI;

public class EquipTutorial : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject nextTutorial;

    public void OnClick()
    {
        tutorial.SetActive(false);
        if (nextTutorial != null)
        {
            nextTutorial.SetActive(true);
            Inventory.Instance.currentDataSave.isCompletedManageTutorial = true;
        }
    }
}
