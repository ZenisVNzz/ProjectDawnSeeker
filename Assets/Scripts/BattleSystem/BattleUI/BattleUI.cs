using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public GameObject turnTitle;
    public List<CharacterRuntime> activeCharacter;
    public List<CharacterRuntime> activeEnemyCharacter;
    public List<GameObject> CharPanel;
    public List<GameObject> skillButtons;
    public SelectSkill selectSkill;
    private Dictionary<CharacterRuntime, float> oldHP = new Dictionary<CharacterRuntime, float>();
    private Dictionary<CharacterRuntime, GameObject> CharPanelDict = new Dictionary<CharacterRuntime, GameObject>();
    private GameObject oldCharPanel;
    private int currentTurn;

    void Start()
    {
        LocalizeStringEvent text = turnTitle.transform.Find("Turn").GetComponent<LocalizeStringEvent>();
        text.StringReference.Arguments = new object[] {
        new {
             turnIndex = currentTurn + 1
            }
        };
        text.RefreshString();

        for (int i = activeCharacter.Count; i < CharPanel.Count; i++)
        {
            CharPanel[i].SetActive(false);
        }

        for (int i = 0; i < activeCharacter.Count; i++)
        {
            CharPanelDict.Add(activeCharacter[i], CharPanel[i]);
        }
    } 

    public void RefreshBattleUI()
    {
        int i = 0;
        foreach (CharacterRuntime activeCharacter in activeCharacter)
        {
            Slider healthSlider = CharPanel[i].transform.Find("HealthBar").transform.Find("Slider").GetComponent<Slider>();
            Slider manaSlider = CharPanel[i].transform.Find("ManaBar").transform.Find("Slider").GetComponent<Slider>();
            TextMeshProUGUI healthText = CharPanel[i].transform.Find("HealthBar").transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI manaText = CharPanel[i].transform.Find("ManaBar").transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            GameObject healthBarDelay = CharPanel[i].transform.Find("HealthBar").transform.Find("HealthBarDelay").gameObject;
            Transform healthBar = CharPanel[i].transform.Find("HealthBar").transform;
            Image charIMG = CharPanel[i].transform.Find("CharIcon").transform.Find("CharIMG").GetComponent<Image>();
            GameObject healthBarObj = Instantiate(healthBarDelay, healthBar.transform);
            healthBarObj.transform.SetSiblingIndex(0);
            healthBarObj.SetActive(true);
            Slider slider = healthBarObj.transform.Find("Slider").GetComponent<Slider>();
            slider.maxValue = activeCharacter.HP;
            if (oldHP.ContainsKey(activeCharacter))
            {
                slider.value = oldHP[activeCharacter];
            }
            else
            {
                slider.value = activeCharacter.currentHP;
            }
            Destroy(healthBarObj, 0.8f);
            charIMG.sprite = activeCharacter.characterSprite;
            healthSlider.maxValue = activeCharacter.HP;
            healthSlider.value = activeCharacter.currentHP;
            manaSlider.maxValue = activeCharacter.MP;
            manaSlider.value = activeCharacter.currentMP;
            float HP = Mathf.Round(activeCharacter.currentHP * 100f) / 100f;
            healthText.text = $"{HP}/{activeCharacter.HP}";
            manaText.text = $"{activeCharacter.currentMP}/{activeCharacter.MP}";
            oldHP[activeCharacter] = activeCharacter.currentHP;

            if (!activeCharacter.isAlive)
            {
                charIMG.color = new Color(1, 1, 1, 0.3f);
            }    

            i++;
        }
        foreach (CharacterRuntime enemy in activeEnemyCharacter)
        {
            EnemyStatusBar enemyStatusBar = enemy.transform.Find("StatusBar").GetComponent<EnemyStatusBar>();
            enemyStatusBar.InitializeStatus();
        }    
    }
    
    public void RefreshTurnUI(int currentTurn)
    {
        Animator animator = turnTitle.GetComponent<Animator>();
        animator.Play("NextTurn");
        this.currentTurn = currentTurn;
    }

    public void ChangeTurn()
    {
        LocalizeStringEvent text = turnTitle.transform.Find("Turn").GetComponent<LocalizeStringEvent>();
        text.StringReference.Arguments = new object[] {
        new {
             turnIndex = currentTurn
            }
        };
        text.RefreshString();
    }

    public void ShowSkillUI(CharacterRuntime owner)
    {
        selectSkill.SetOriginalParent();
        for (int i = 0; i < owner.skillList.Count; i++)
        {
            Image image = skillButtons[i].transform.Find("IMG").GetComponent<Image>();
            TextMeshProUGUI skillName = skillButtons[i].transform.Find("SkillName").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI mpCost = skillButtons[i].transform.Find("MpCost").GetComponent<TextMeshProUGUI>();
            SelectSkill selectSkill = skillButtons[i].GetComponentInChildren<SelectSkill>();
            skillName.text = owner.skillList[i].localizedSkillName.GetLocalizedString();
            mpCost.text = $"{owner.skillList[i].mpCost} MP";
            selectSkill.skill = owner.skillList[i];
            image.sprite = owner.skillList[i].icon;
            if (skillButtons[i].activeSelf == false)
            {
                skillButtons[i].SetActive(true);
            }
        }
        for (int i = owner.skillList.Count; i < skillButtons.Count; i++)
        {
            skillButtons[i].SetActive(false);
        }
        selectSkill.EnableGridLayout();
    }

    public void SelectingCharacter(CharacterRuntime character)
    {
        if (CharPanelDict.ContainsKey(character))
        {
            Image panelIMG = CharPanelDict[character].GetComponent<Image>();
            if (oldCharPanel != null)
            {
                Image oldPanelIMG = oldCharPanel?.GetComponent<Image>();
                oldPanelIMG.color = new Color(107f / 255f, 111f / 255f, 217f / 255f, 210f / 255f);
            }
            panelIMG.color = new Color(255f / 255f, 154f / 255f, 4f / 255f, 210f / 255f);
            oldCharPanel = CharPanelDict[character];
        }  
    }    
}
