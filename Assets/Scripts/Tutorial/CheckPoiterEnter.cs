using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckPoiterEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(CheckPointerEnter());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(CheckPointerEnter());
    }

    private IEnumerator CheckPointerEnter()
    {
        yield return new WaitForSeconds(2f);
        Tutorial6 tutorial6 = FindAnyObjectByType<Tutorial6>();
        tutorial6.OnCompleted();
        Destroy(gameObject.GetComponent<CheckPoiterEnter>());
    }
}
