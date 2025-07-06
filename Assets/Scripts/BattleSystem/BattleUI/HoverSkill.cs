using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverSkill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject outline;
    public Image image;
    public Color flashColor;
    public float flashDuration = 0.5f;
    private Color originalColor;

    void Start()
    {
        originalColor = image.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        outline.SetActive(true);
        image.DOKill();
        image.DOColor(flashColor, flashDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.DOKill();
        image.color = originalColor;
        outline.SetActive(false);
    }
}
