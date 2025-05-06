using UnityEngine;
using UnityEngine.UI;

public class SelectSkill : MonoBehaviour
{
    public static CharacterInBattle characterInBattle;
    public SkillBase skill;
    public BattleManager battleManager;
    public static bool isPlayerSelectingTarget = false;
    public static SkillBase selectedSkill;

    void Start()
    {
        Button button = GetComponent<Button>();
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
            }
            else
            {
                isPlayerSelectingTarget = false;
                selectedSkill = null;
                Debug.Log("Đã bỏ chọn skill: " + this.skill.name);
            }
        });
    }

    public SkillBase GetSkillBase()
    {
        return selectedSkill;
    }    
}
