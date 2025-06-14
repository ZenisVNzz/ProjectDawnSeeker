using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowInfoOnClick : MonoBehaviour
{
    public Image charIMG;
    public TextMeshProUGUI charName;
    public TextMeshProUGUI charLevel;
    public TextMeshProUGUI hpIndex;
    public TextMeshProUGUI defIndex;
    public TextMeshProUGUI atkIndex;
    public TextMeshProUGUI mpIndex;
    public TextMeshProUGUI crIndex;
    public TextMeshProUGUI cdIndex;
    public TextMeshProUGUI dcIndex;
    public TextMeshProUGUI pcIndex;

    public List<GameObject> skillPanel;

    private CharDataStorage charDataStorage;
    public GameObject characterInfo;

    public GameObject equipButton;
    public GameObject unEquipButton;
    private EquipedUnit equipedUnit;

    private void Start()
    {
        charDataStorage = GetComponent<CharDataStorage>();
        Button button = GetComponent<Button>();
        equipedUnit = FindAnyObjectByType<EquipedUnit>();

        button.onClick.AddListener(() =>
        {
            ShowInfoCharacter();
        });
    }

    public void ShowInfoCharacter()
    {
        if (EquipedUnit.equipedUnit.Contains(charDataStorage.characterData))
        {
            equipButton.SetActive(false);
            unEquipButton.SetActive(true);
        }
        else
        {
            equipButton.SetActive(true);
            unEquipButton.SetActive(false);
        }
        CharDataStorage charData = characterInfo.GetComponent<CharDataStorage>();
        charData.characterData = charDataStorage.characterData;

        foreach (GameObject gameObj in skillPanel)
        {
            gameObj.SetActive(false);
        }

        characterInfo.SetActive(true);

        charIMG.sprite = charDataStorage.characterData.characterSprite;
        charLevel.text = $"lv.{charDataStorage.characterData.level}";
        charName.text = charDataStorage.characterData.localizedCharacterName.GetLocalizedString();
        hpIndex.text = charDataStorage.characterData.HP.ToString("0.#");
        defIndex.text = charDataStorage.characterData.DEF.ToString("0.#");
        atkIndex.text = charDataStorage.characterData.ATK.ToString("0.#");
        mpIndex.text = charDataStorage.characterData.MP.ToString("0.#");
        crIndex.text = (Mathf.Round(charDataStorage.characterData.CR * 100) + "%").ToString();
        cdIndex.text = (Mathf.Round(charDataStorage.characterData.CD * 100) + "%").ToString();
        dcIndex.text = (Mathf.Round(charDataStorage.characterData.DC * 100) + "%").ToString();
        pcIndex.text = (Mathf.Round(charDataStorage.characterData.PC * 100) + "%").ToString();

        for (int i = 0; i < charDataStorage.characterData.skillList.Count; i++)
        {
            skillPanel[i].SetActive(true);
            Image skillImage = skillPanel[i].transform.Find("SkillOutline/SkIMG").GetComponent<Image>();
            TextMeshProUGUI skillName = skillPanel[i].transform.Find("SkillName").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI mpCost = skillPanel[i].transform.Find("MpCost").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI skillDes = skillPanel[i].transform.Find("Des").GetComponent<TextMeshProUGUI>();
            skillImage.sprite = charDataStorage.characterData.skillList[i].icon;
            skillName.text = charDataStorage.characterData.skillList[i].localizedSkillName.GetLocalizedString();
            mpCost.text = ($"Mp cost {charDataStorage.characterData.skillList[i].mpCost.ToString()}");
            skillDes.text = charDataStorage.characterData.skillList[i].localizedDescription.GetLocalizedString();
        }
        UpgradeChar upgradeChar = FindAnyObjectByType<UpgradeChar>();
        upgradeChar.showInfoOnClick = this;
    }    
}
