using UnityEngine;
using UnityEngine.UI;

public class SettingsDisplayButton : MonoBehaviour
{
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            GameManager.Instance.DisplaySettings();
        });
    }
}
