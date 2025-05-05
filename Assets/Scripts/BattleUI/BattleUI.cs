using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject manaBar;
    public CharacterInBattle characterInBattle;
    private float oldHP;

    public void RefreshBattleUI()
    {
        Slider healthSlider = healthBar.transform.Find("Slider").GetComponent<Slider>();
        Slider manaSlider = manaBar.transform.Find("Slider").GetComponent<Slider>();
        TextMeshProUGUI healthText = healthBar.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI manaText = manaBar.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        GameObject healthBarDelay = healthBar.transform.Find("HealthBarDelay").gameObject;
        GameObject healthBarObj = Instantiate(healthBarDelay, healthBar.transform);
        healthBarObj.transform.SetSiblingIndex(0);
        healthBarObj.SetActive(true);
        Slider slider = healthBarObj.transform.Find("Slider").GetComponent<Slider>();
        slider.maxValue = characterInBattle.HP;
        slider.value = oldHP;
        Destroy(healthBarObj, 0.8f);
        healthSlider.maxValue = characterInBattle.HP;
        healthSlider.value = characterInBattle.currentHP;
        manaSlider.maxValue = characterInBattle.MP;
        manaSlider.value = characterInBattle.currentMP;
        healthText.text = $"{characterInBattle.currentHP}/{characterInBattle.HP}";
        manaText.text = $"{characterInBattle.currentMP}/{characterInBattle.MP}";
        oldHP = characterInBattle.currentHP;
    }    
}
