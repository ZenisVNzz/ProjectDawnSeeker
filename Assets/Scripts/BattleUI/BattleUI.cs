using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject manaBar;
    public CharacterInBattle characterInBattle;

    public void RefreshBattleUI()
    {
        Slider healthSlider = healthBar.transform.Find("Slider").GetComponent<Slider>();
        Slider manaSlider = manaBar.transform.Find("Slider").GetComponent<Slider>();
        TextMeshProUGUI healthText = healthBar.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI manaText = manaBar.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        healthSlider.maxValue = characterInBattle.HP;
        healthSlider.value = characterInBattle.currentHP;
        manaSlider.maxValue = characterInBattle.MP;
        manaSlider.value = characterInBattle.currentMP;
        healthText.text = $"{characterInBattle.currentHP}/{characterInBattle.HP}";
        manaText.text = $"{characterInBattle.currentMP}/{characterInBattle.MP}";
    }    
}
