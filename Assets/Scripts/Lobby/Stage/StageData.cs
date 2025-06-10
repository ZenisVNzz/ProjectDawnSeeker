using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageData : MonoBehaviour
{
    public int stageID;
    public string stageName;
    public List<Enemy> enemies;
    public List<Item> items;
    public int goldReward;
    public int expGainForEachChar;
    public bool bossLevel;
    public bool isUnlock;

    public static int currentStage;

    void Start()
    {
        UpdateStage();
    }

    public void UpdateStage()
    {
        if (currentStage >= stageID)
        {
            if (SceneManager.GetActiveScene().name == "DUNGEON")
            {
                isUnlock = true;
                transform.Find("Lock").gameObject.SetActive(false);
            }
        }
    }

    public void UnlockNextStage()
    {
        if (stageID == currentStage)
        {
            currentStage++;
            Debug.Log($"Unlock stage: {currentStage}");
            Inventory.Instance.currentDataSave.currentStage = currentStage;    
        }
        Inventory.Instance.SaveGame();
    }
}
