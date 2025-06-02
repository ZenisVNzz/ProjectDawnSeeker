using UnityEngine;
using UnityEngine.EventSystems;

public class DisplaySelecting : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject selectingObj;

    public void OnPointerEnter(PointerEventData eventData)
    {
        selectingObj.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        selectingObj.SetActive(false);
    }
}
