using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class SelectLevel : MonoBehaviour
{
    public GameObject notification;

    private void Start()
    {
        Button button = GetComponent<Button>();
        StageData stageData = gameObject.GetComponent<StageData>();
        button.onClick.AddListener(() =>
        {
            if (EquipedUnit.equipedUnit.Count <= 0)
            {
                notification.SetActive(true);
                return;
            }    
            if (stageData.isUnlock)
            {
                GameManager gameManager = FindAnyObjectByType<GameManager>();
                StageData currentStageData = gameManager.transform.Find("StageData").GetComponent<StageData>();

                foreach (Enemy enemy in stageData.enemies)
                {
                    CharacterData enemyData = enemy.characterData;
                    CharacterData enemyRuntime = Instantiate(enemyData);
                    enemyRuntime.AddXP(GetXPNeededForLevel(enemy.level));
                    enemy.characterData = enemyRuntime;
                }

                currentStageData.stageID = stageData.stageID;
                currentStageData.stageName = stageData.stageName;
                currentStageData.enemies.Clear();
                currentStageData.enemies = stageData.enemies;
                currentStageData.items.Clear();
                currentStageData.items = stageData.items;
                currentStageData.goldReward = stageData.goldReward;
                currentStageData.expGainForEachChar = stageData.expGainForEachChar;
                currentStageData.bossLevel = stageData.bossLevel;
                currentStageData.isUnlock = stageData.isUnlock;
                currentStageData.bgmClip = stageData.bgmClip;
                currentStageData.bgmLoop = stageData.bgmLoop;
                SceneManager.LoadScene("Battle");
            }         
        });
    }

    int GetXPNeededForLevel(int targetLevel)
    {
        float xp = 100f;
        float totalXP = 0;
        for (int i = 1; i < targetLevel; i++)
        {
            xp = Mathf.Round(xp);
            totalXP += xp;
            xp *= 1.04f;
        }
        return Mathf.RoundToInt(totalXP);
    }
}
