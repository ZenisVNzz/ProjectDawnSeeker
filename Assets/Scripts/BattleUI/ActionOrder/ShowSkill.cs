using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowSkill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject skill;
    private Transform panel;
    public GameObject parent;
    public Transform originalParent;
    private TargetArrow targetArrow;
    private DataStorage dataStorage;
    private CanvasGroup canvasGroup;
    private bool canPointerEnter;

    void Start()
    {
        canPointerEnter = false;
        skill = parent.transform.Find("Skill").gameObject;
        panel = parent.transform.parent.transform.parent;
        targetArrow = FindAnyObjectByType<TargetArrow>();
        dataStorage = GetComponent<DataStorage>();
        canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(DelayPoiterEnter());
    }

    IEnumerator DelayPoiterEnter()
    {
        yield return new WaitForSeconds(1f);
        canPointerEnter = true;
    }    

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (canPointerEnter)
        {
            targetArrow.RemoveArrow();
            skill.transform.SetParent(panel);
            if (dataStorage.attacker != dataStorage.target)
            {
                targetArrow.MakeArrow(dataStorage.attacker, dataStorage.target, dataStorage.isTargetAlly);
            }
            skill.SetActive(true);
        }        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        skill.transform.SetParent(originalParent);
        targetArrow.RemoveArrow();
        skill.SetActive(false);
    }

    public void ForceExit()
    {
        skill.transform.SetParent(originalParent);
        targetArrow.RemoveArrow();
        skill.SetActive(false);
    }

    public IEnumerator DisableInteraction()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        skill.transform.SetParent(originalParent);
        yield return new WaitForSeconds(0.8f);
        if (canvasGroup != null)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }       
    }    
}
