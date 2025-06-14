using TMPro;
using UnityEngine;
using System.Collections;

public class TypeWriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float typingSpeed = 0.05f;
    [TextArea]
    public string fullText;

    private void Start()
    {
        fullText = textComponent.text;
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        textComponent.text = "";
        foreach (char c in fullText)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
