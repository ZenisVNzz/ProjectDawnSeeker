using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayStatInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject InfoObj;

    public void OnPointerEnter(PointerEventData eventData)
    {
        InfoObj.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        InfoObj.SetActive(false);
    }
}
