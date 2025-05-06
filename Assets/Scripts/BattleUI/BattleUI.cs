using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public GameObject turnTitle;
    public List<CharacterInBattle> activeCharacter;
    public List<CharacterInBattle> activeEnemyCharacter;
    public List<GameObject> CharPanel;
    public List<GameObject> skillButtons;
    private Dictionary<CharacterInBattle, float> oldHP = new Dictionary<CharacterInBattle, float>();
    private int currentTurn;

    public void RefreshBattleUI()
    {
        int i = 0;
        foreach (CharacterInBattle activeCharacter in activeCharacter)
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
        foreach (CharacterInBattle enemy in activeEnemyCharacter)
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
        TextMeshProUGUI text = turnTitle.transform.Find("Turn").GetComponent<TextMeshProUGUI>();
        text.text = $"TURN {currentTurn}";
    }

    public void ShowSkillUI(CharacterInBattle owner)
    {
        for (int i = 0; i < owner.skillList.Count; i++)
        {
            Image image = skillButtons[i].transform.Find("IMG").GetComponent<Image>();
            TextMeshProUGUI skillName = skillButtons[i].transform.Find("SkillName").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI mpCost = skillButtons[i].transform.Find("MpCost").GetComponent<TextMeshProUGUI>();
            SelectSkill selectSkill = skillButtons[i].GetComponent<SelectSkill>();
            skillName.text = owner.skillList[i].skillName;
            mpCost.text = $"{owner.skillList[i].mpCost} MP";
            selectSkill.skill = owner.skillList[i];
            image.sprite = owner.skillList[i].icon;
        }
    }
}
