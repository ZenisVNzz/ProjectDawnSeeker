using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionUI_FloatIn : MonoBehaviour
{
    public RectTransform target;
    public CanvasGroup canvasGroup;
    public Vector2 offset = new Vector2(50f, 0f);
    public float duration = 0.5f;
    private HorizontalLayoutGroup horizontalLayoutGroup;

    void OnEnable()
    {
        horizontalLayoutGroup = GetComponentInParent<HorizontalLayoutGroup>();
        canvasGroup.alpha = 0;
        StartCoroutine(FloatIn());
    }

    public IEnumerator FloatIn()
    {
        yield return new WaitForSeconds(0f);
        horizontalLayoutGroup.enabled = false;
        yield return new WaitForSeconds(0f);
        target.anchoredPosition += offset;
        canvasGroup.alpha = 0;

        target.DOAnchorPos(target.anchoredPosition - offset, duration).SetEase(Ease.OutCubic);
        canvasGroup.DOFade(1f, duration);
        yield return new WaitForSeconds(0f);
        horizontalLayoutGroup.enabled = true;
    }

    public IEnumerator FloatOut(GameObject gameObj)
    {
        yield return new WaitForSeconds(0f);
        horizontalLayoutGroup.enabled = false;
        yield return new WaitForSeconds(0f);

        target.DOAnchorPos(target.anchoredPosition + new Vector2(-Mathf.Abs(offset.x), offset.y), duration)
          .SetEase(Ease.InCubic);
        canvasGroup.DOFade(0f, duration);

        yield return new WaitForSeconds(0.3f);
        Destroy(gameObj);
        horizontalLayoutGroup.enabled = true;
    }    
}
