using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Image healthBarImage;
    public CharacterInBattle character;

    void Update()
    {
        if (character != null && healthBarImage != null)
        {
            float ratio = character.currentHP / character.HP;
            healthBarImage.fillAmount = Mathf.Clamp01(ratio);
        }
    }
}