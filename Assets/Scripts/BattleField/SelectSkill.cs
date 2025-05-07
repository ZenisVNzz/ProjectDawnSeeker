using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkill : MonoBehaviour
{
    public static CharacterInBattle characterInBattle;
    public SkillBase skill;
    public BattleManager battleManager;
    public static bool isPlayerSelectingTarget = false;
    public static SkillBase selectedSkill;
    public static GameObject currentSkillBox;
    public static GridLayoutGroup gridLayoutGroup;
    public static List<GameObject> skillBoxList = new List<GameObject>();

    private Transform originalParent;
    private static Dictionary<GameObject, int> siblingIndex = new Dictionary<GameObject, int>();
    private GameObject skillBox;

    void Start()
    {     
        Button button = GetComponent<Button>();
        skillBox = transform.gameObject;

        if (!skillBoxList.Contains(skillBox))
        {
            skillBoxList.Add(skillBox);
        }
        foreach (GameObject skillBox in skillBoxList)
        {
            if (!siblingIndex.ContainsKey(skillBox))
            {
                siblingIndex.Add(skillBox, skillBox.transform.GetSiblingIndex());
            }
        }

        gridLayoutGroup = transform.parent.GetComponent<GridLayoutGroup>();
        originalParent = skillBox.transform.parent;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            if (selectedSkill == null || selectedSkill != this.skill)
            {
                if (characterInBattle.currentMP < this.skill.mpCost)
                {
                    Debug.Log("Không đủ mana để sử dụng skill: " + this.skill.name);
                    return;
                }
                selectedSkill = this.skill;
                isPlayerSelectingTarget = true;
                Debug.Log("Đã chọn skill: " + this.skill.name);

                if (currentSkillBox != null && currentSkillBox != this.skillBox)
                {
                    currentSkillBox.GetComponent<Animator>().Play("UnselectSkill");
                }
                gridLayoutGroup.enabled = false;        

                skillBox.transform.SetParent(originalParent.parent);
                Animator animator = skillBox.GetComponent<Animator>();
                animator.Play("SelectSkill");
                currentSkillBox = skillBox;
            }
            else
            {
                isPlayerSelectingTarget = false;
                selectedSkill = null;
                Debug.Log("Đã bỏ chọn skill: " + this.skill.name);

                if (currentSkillBox != null)
                {
                    currentSkillBox.GetComponent<Animator>().Play("UnselectSkill");
                }
            }
        });
    }

    public SkillBase GetSkillBase()
    {
        return selectedSkill;
    } 
    
    public void SetOriginalParent()
    {
        foreach (GameObject skillBox in skillBoxList)
        {
            if (siblingIndex.ContainsKey(skillBox))
            {
                skillBox.transform.SetParent(originalParent);
                skillBox.transform.SetSiblingIndex(siblingIndex[skillBox]);
            }
        }
        currentSkillBox = null;
    }     

    public void EnableGridLayout()
    {
        foreach (GameObject skillBox in skillBoxList)
        {
            Animator animator = skillBox.GetComponent<Animator>();
            animator.Rebind();
            animator.Update(0f);
        }
        if (gridLayoutGroup != null)
        {
            gridLayoutGroup.enabled = true;
        }      
    }    
}
