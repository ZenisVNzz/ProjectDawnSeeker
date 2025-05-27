using DG.Tweening;
using UnityEngine;
using TMPro;

public class GradientText : MonoBehaviour
{
    public TextMeshProUGUI tmpText;
    public Gradient gradient;
    public float duration = 2f;

    void Start()
    {
        DOTween.To(() => 0f, t =>
        {
            tmpText.color = gradient.Evaluate(t);
        }, 1f, duration).SetLoops(-1, LoopType.Restart);
    }
}
