using UnityEngine;
using UnityEngine.UI;

public class EquipTutorial : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject nextTutorial;

    void Start()
    {
        if (Inventory.Instance.currentDataSave.isCompletedTarvenTutorial && !Inventory.Instance.currentDataSave.isCompletedManageTutorial)
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }
    }

    public void OnClick()
    {
        tutorial.SetActive(false);
        if (nextTutorial != null)
        {
            nextTutorial.SetActive(true);
        }
        Inventory.Instance.currentDataSave.isCompletedManageTutorial = true;
        Inventory.Instance.SaveGame();
    }
}
