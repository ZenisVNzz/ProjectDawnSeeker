using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

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

    public static int currentStage = 500001;

    void Start()
    {
        UpdateStage();
    }

    public void UpdateStage()
    {
        if (currentStage >= stageID)
        {
            isUnlock = true;
        }
    }

    public void UnlockNextStage()
    {
        currentStage++;
    }
}
