using UnityEngine;
using UnityEngine.UI;

public class ToggleX2 : MonoBehaviour
{
    public bool isOn;

    void Start()
    {
        Button button = GetComponent<Button>();
        Image image = transform.Find("Outline").GetComponent<Image>();
        Color offColor = Color.white;
        Color onColor = new Color(136f / 255f, 141f / 255f, 1f, 1f);

        button.onClick.AddListener(() =>
        {
            if (image.color == offColor)
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
