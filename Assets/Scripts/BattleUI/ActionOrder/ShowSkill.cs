using UnityEngine;
using UnityEngine.EventSystems;

public class ShowSkill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject skill;
    private Transform panel;
    public GameObject parent;
    public Transform originalParent;
    private TargetArrow targetArrow;
    private DataStorage dataStorage;

    void Start()
    {
        skill = parent.transform.Find("Skill").gameObject;
        panel = parent.transform.parent.transform.parent;
        targetArrow = FindAnyObjectByType<TargetArrow>();
        dataStorage = GetComponent<DataStorage>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetArrow.RemoveArrow();
        skill.transform.SetParent(panel);
        if (!dataStorage.isPassiveSkill)
        {
            targetArrow.MakeArrow(dataStorage.attacker, dataStorage.target, dataStorage.isTargetAlly);
        }     
        skill.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        skill.transform.SetParent(originalParent);
        targetArrow.RemoveArrow();
        skill.SetActive(false);
    }
}
