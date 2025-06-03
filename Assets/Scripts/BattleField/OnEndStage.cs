using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnEndStage : MonoBehaviour
{
    public GameObject victoryPanel;
    public GameObject failedPanel;
    public GameObject itemPrefab;
    public GameObject charExpPrefab;

    private BattleManager battleManager;

    private void Start()
    {
        battleManager = FindAnyObjectByType<BattleManager>();
        battleManager.OnFinishedStage += EndStage;
    }

    public void EndStage(bool victory)
    {
        if (victory)
        {
            Transform itemContainer = victoryPanel.transform.Find("Information/Reward/ScrollView/Viewport/Content");
            Transform xpContainer = victoryPanel.transform.Find("Information/Exp/Viewport/Content");
            TextMeshProUGUI completedText = victoryPanel.transform.Find("Information/Completed").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI turnIndex = victoryPanel.transform.Find("Information/Turn/Number").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI goldIndex = victoryPanel.transform.Find("Information/GoldReward/Number").GetComponent<TextMeshProUGUI>();

            GameManager gameManager = FindAnyObjectByType<GameManager>();
            StageData stageData = gameManager.transform.Find("StageData").GetComponent<StageData>();

            completedText.text = $"BẠN ĐÃ HOÀN THÀNH {stageData.stageName}";
            turnIndex.text = battleManager.GetCurrentTurn().ToString();
            goldIndex.text = stageData.goldReward.ToString();
            Inventory.Instance.AddMoney(stageData.goldReward);

            foreach (Item item in stageData.items)
            {
                ItemBase itemBase = item.item;
                GameObject itemObj = Instantiate(itemPrefab, itemContainer);
                Image icon = itemObj.transform.Find("Icon").GetComponent<Image>();
                TextMeshProUGUI itemQuantity = itemObj.transform.Find("Amount").GetComponent<TextMeshProUGUI>();

                icon.sprite = itemBase.itemIcon;
                itemQuantity.text = item.quantity.ToString();
                for (int i = 0; i < item.quantity; i++)
                {
                    Inventory.Instance.AddItem(itemBase);
                }
            }
            foreach (CharacterData character in EquipedUnit.equipedUnit)
            {
                character.AddXP(stageData.expGainForEachChar);
                GameObject charExpObj = Instantiate(charExpPrefab, xpContainer);
                TextMeshProUGUI charName = charExpObj.transform.Find("CharName").GetComponent<TextMeshProUGUI>();
                Image charIMG = charExpObj.transform.Find("CharIMG/IMG").GetComponent<Image>();
                Slider expSlider = charExpObj.transform.Find("XPBar").GetComponent<Slider>();
                TextMeshProUGUI levelText = charExpObj.transform.Find("XPBar/Level").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI expText = charExpObj.transform.Find("XPBar/EP").GetComponent<TextMeshProUGUI>();
                expSlider.maxValue = character.neededXP;
                expSlider.value = character.currentXP;
                levelText.text = $"Level {character.level}";
                expText.text = $"{character.currentXP}/{character.neededXP} (+{stageData.expGainForEachChar})";
            }
            stageData.UnlockNextStage();
        }
        else
        {
            GameManager gameManager = FindAnyObjectByType<GameManager>();
            StageData stageData = gameManager.transform.Find("StageData").GetComponent<StageData>();
            TextMeshProUGUI failedText = failedPanel.transform.Find("Failed").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI turnIndex = victoryPanel.transform.Find("Turn/Number").GetComponent<TextMeshProUGUI>();
            failedText.text = $"BẠN ĐÃ THẤT BẠI TRONG VIỆC CHINH PHỤC {stageData.stageName}";
            turnIndex.text = battleManager.GetCurrentTurn().ToString();
        }
    }
}
