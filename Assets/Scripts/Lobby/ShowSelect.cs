using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject gameObj;

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObj.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObj.SetActive(false);
    }
}
