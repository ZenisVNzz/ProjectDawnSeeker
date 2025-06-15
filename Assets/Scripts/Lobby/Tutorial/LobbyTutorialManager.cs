using UnityEngine;

public class LobbyTutorialManager : MonoBehaviour
{
    [Header("TarvenTutorial")]
    public GameObject tutorial_1;
    public GameObject doneTarvenTutorial;

    [Header("ManageTutorial")]
    public GameObject tutorial_2;

    [Header("BattleTutorial")]
    public GameObject tutorial_3;

    void Start()
    {
        GameManager gameManager = GameManager.Instance;
        Inventory inventory = Inventory.Instance;
        if (!inventory.currentDataSave.isCompletedTarvenTutorial)
        {
            if (Inventory.Instance.gold <= 0)
            {
                Inventory.Instance.AddMoney(100);
            }
            if (tutorial_1 != null)
            {
                tutorial_1.SetActive(true);
            }
        }  
        if (inventory.currentDataSave.isCompletedTarvenTutorial && !inventory.currentDataSave.isCompletedManageTutorial)
        {
            if (tutorial_2 != null)
            {
                tutorial_2.SetActive(true);
            }           
        }
        if (inventory.currentDataSave.isCompletedTarvenTutorial && inventory.currentDataSave.isCompletedManageTutorial)
        {
            if (tutorial_3 != null)
            {
                tutorial_3.SetActive(true);
            }
        }
    }

    public void DoneTarvenTutorial()
    {
        if (doneTarvenTutorial != null)
        {
            doneTarvenTutorial.SetActive(true);
        }
    }
}
