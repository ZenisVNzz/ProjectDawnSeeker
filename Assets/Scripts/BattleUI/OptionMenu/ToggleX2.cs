using UnityEngine;
using UnityEngine.UI;

public class ToggleX2 : MonoBehaviour
{
    public bool isOn;

    void Start()
    {
        Button button = GetComponentInChildren<Button>();
        Image image = transform.Find("Outline").GetComponent<Image>();
        Color offColor = new Color(255f / 255f, 160f / 255f, 109f / 255f, 1f);
        Color onColor = new Color(97f / 255f, 194f / 255f, 1f, 1f);

        button.onClick.AddListener(() =>
        {
            if (!isOn)
            {
                image.color = onColor;
                isOn = true;
            }
            else
            {
                image.color = offColor;
                isOn = false;
            }
        });
    }
}
