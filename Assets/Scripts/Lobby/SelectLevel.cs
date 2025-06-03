using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            StageData stageData = gameObject.GetComponent<StageData>();
            if (stageData.isUnlock)
            {
                GameManager gameManager = FindAnyObjectByType<GameManager>();
                StageData currentStageData = gameManager.transform.Find("StageData").GetComponent<StageData>();
                currentStageData.stageID = stageData.stageID;
                currentStageData.stageName = stageData.stageName;
                currentStageData.enemies = stageData.enemies;
                currentStageData.items = stageData.items;
                currentStageData.goldReward = stageData.goldReward;
                currentStageData.expGainForEachChar = stageData.expGainForEachChar;
                currentStageData.bossLevel = stageData.bossLevel;
                currentStageData.isUnlock = stageData.isUnlock;

                foreach (Enemy character in stageData.enemies)
                {
                    CharacterData enemyData = character.characterData;
                    CharacterData enemyDataRunTime = Instantiate(enemyData);
                    enemyDataRunTime.AddXP(GetXPNeededForLevel(character.level));
                    character.characterData = enemyDataRunTime;
                }
                if (gameObject.name == "Lvl1")
                {
                    SceneManager.LoadScene("TutorialBattle");
                }
                else
                {
                    SceneManager.LoadScene("Battle");
                }       
            }         
        });
    }

    int GetXPNeededForLevel(int level)
    {
        float baseXP = 100f;
        float xp = baseXP;
        for (int i = 1; i < level; i++)
        {
            xp *= 1.04f;
            xp = Mathf.Round(xp);
        }
        return (int)xp;
    }
}
