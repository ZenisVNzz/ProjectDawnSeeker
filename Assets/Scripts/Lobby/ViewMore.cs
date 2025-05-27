using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class ViewMore : MonoBehaviour
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
    private CharDataStorage charDataStorage;
    private bool isPreviewingMaxLevel;

    public GameObject characterInfo;
    public List<GameObject> skillPanel;

    private void Start()
    {
        charDataStorage = GetComponent<CharDataStorage>();
        Button button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            ShowInfoCharacter();
        });
    }

    public void ShowInfoCharacter()
    {
        isPreviewingMaxLevel = false;

        CharDataStorage charData = characterInfo.GetComponent<CharDataStorage>();
        charData.characterData = charDataStorage.characterData;

        foreach (GameObject gameObj in skillPanel)
        {
            gameObj.SetActive(false);
        }

        characterInfo.SetActive(true);

        charIMG.sprite = charDataStorage.characterData.characterSprite;
        charLevel.text = $"lv.{charDataStorage.characterData.level}";
        charName.text = charDataStorage.characterData.characterName;
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
            skillName.text = charDataStorage.characterData.skillList[i].skillName;
            mpCost.text = ($"Mp cost {charDataStorage.characterData.skillList[i].mpCost.ToString()}");
            skillDes.text = charDataStorage.characterData.skillList[i].description;
        }
        PreviewMaxLevel();
    }

    public void PreviewMaxLevel()
    {
        Button button = characterInfo.transform.Find("PreviewLevel").GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            if (!isPreviewingMaxLevel)
            {
                float hp = charDataStorage.characterData.HP;
                float def = charDataStorage.characterData.DEF;
                float atk = charDataStorage.characterData.ATK;
                float mp = charDataStorage.characterData.MP;
                float cr = charDataStorage.characterData.CR;
                float cd = charDataStorage.characterData.CD;

                hp += ((charDataStorage.characterData.HPPerLevel) * 99);
                def += ((charDataStorage.characterData.DEFPerLevel) * 99);
                atk += ((charDataStorage.characterData.ATKPerLevel) * 99);
                mp += ((charDataStorage.characterData.MPPerLevel) * 99);
                cr += ((charDataStorage.characterData.CRPerLevel) * 99);
                cd += ((charDataStorage.characterData.CDPerLevel) * 99);

                charLevel.text = "lv.100";
                hpIndex.text = hp.ToString("0.#");
                defIndex.text = def.ToString("0.#");
                atkIndex.text = atk.ToString("0.#");
                mpIndex.text = mp.ToString("0.#");
                crIndex.text = (Mathf.Round(cr * 100) + "%").ToString();
                cdIndex.text = (Mathf.Round(cd * 100) + "%").ToString();
                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = "XEM CẤP 1";
                isPreviewingMaxLevel = true;
            }
            else
            {
                ShowInfoCharacter();
                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = "XEM CẤP 100";
                isPreviewingMaxLevel = false;
            }    
        });
    }    
}
