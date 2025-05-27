using UnityEngine;
using UnityEngine.UI;

public class ClickUpgrade : MonoBehaviour
{
    private void Start()
    {
        Button button = GetComponent<Button>();
        UpgradeChar upgradeChar = FindAnyObjectByType<UpgradeChar>();
        CharDataStorage characterData = GetComponentInParent<CharDataStorage>();
        button.onClick.AddListener(() =>
        {
            upgradeChar.ShowUI(characterData.characterData);
        });
    }
}
