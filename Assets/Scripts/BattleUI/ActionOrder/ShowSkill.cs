using UnityEngine;
using UnityEngine.EventSystems;

public class ShowSkill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject skill;
    private Transform panel;
    public GameObject parent;
    public Transform originalParent;

    void Start()
    {
        skill = parent.transform.Find("Skill").gameObject;
        panel = parent.transform.parent.transform.parent;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        skill.transform.SetParent(panel);
        skill.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        skill.transform.SetParent(originalParent);
        skill.SetActive(false);
    }
}
