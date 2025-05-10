using NUnit.Framework;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Jobs;

public class SelectSkill : MonoBehaviour
{
    public static CharacterInBattle characterInBattle;
    public SkillBase skill;
    public BattleManager battleManager;
    public static bool isPlayerSelectingTarget = false;
    public static SkillBase selectedSkill;
    public static GameObject currentSkillBox;
    public static GridLayoutGroup gridLayoutGroup;
    public static CanvasGroup canvasGroup;
    public static List<GameObject> skillBoxList = new List<GameObject>();
    public static Dictionary<GameObject, int> siblingIndex = new Dictionary<GameObject, int>();

    private Transform originalParent;
    private GameObject skillBox;

    void Start()
    {     
        Button button = GetComponentInParent<Button>();
        skillBox = transform.parent.gameObject;
        canvasGroup = GetComponentInParent<CanvasGroup>();

        if (!siblingIndex.ContainsKey(skillBox))
        {
            siblingIndex.Add(skillBox, skillBox.transform.GetSiblingIndex());
        }

        if (!skillBoxList.Contains(skillBox))
        {
            skillBoxList.Add(skillBox);
        }

        gridLayoutGroup = transform.parent.GetComponentInParent<GridLayoutGroup>();
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

                if (this.skill.passiveSkill)
                {
                    isPlayerSelectingTarget = false;
                    battleManager.plannedActions.Add(new PlannedAction
                    {
                        Caster = characterInBattle,
                        Target = characterInBattle,
                        Skill = this.skill
                    });
                    selectedSkill = this.skill;
                }

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

                if (this.skill.passiveSkill)
                {
                    battleManager.plannedActions.RemoveAll(a => a.Caster == characterInBattle && a.Skill == this.skill);
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
            animator.Play("Reset");
        }
        if (gridLayoutGroup != null)
        {
            gridLayoutGroup.enabled = true;
        }
    }

    public void DisableSkillUI()
    {
        SetOriginalParent();
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
        foreach (GameObject skillBox in skillBoxList)
        {
            Animator animator = skillBox.GetComponent<Animator>();
            animator.Play("UnselectSkill");
        }
    }

    public void EnableSkillUI()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        foreach (GameObject skillBox in skillBoxList)
        {
            Animator animator = skillBox.GetComponent<Animator>();
            animator.Play("Reset");
        }
    }    
}
