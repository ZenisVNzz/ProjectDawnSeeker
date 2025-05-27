using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject gameObj;
    public GameObject selectingUI;

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObj.SetActive(true);
        if (selectingUI != null)
        {
            selectingUI.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObj.SetActive(false);
        if (selectingUI != null)
        {
            selectingUI.SetActive(false);
        }
    }
}
