using UnityEngine;
using UnityEngine.UI;

public class CompleteBattleTutorial : MonoBehaviour
{
    void Start()
    {
        if (Inventory.Instance.currentDataSave.isCompletedTarvenTutorial && Inventory.Instance.currentDataSave.isCompletedManageTutorial)
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }
    }

    public void OnClick()
    {
        Inventory.Instance.currentDataSave.isCompletedBattleTutorial = true;
    }
}
