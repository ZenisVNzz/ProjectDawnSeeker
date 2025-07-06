using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Rendering;

public enum SkillType { Attack, Buff, Debuff, Heal, Aoe, Support, Charge}

public abstract class SkillBase : ScriptableObject
{
    public int ID;
    public string skillName;
    public LocalizedString localizedSkillName;
    public string description;
    public LocalizedString localizedDescription;
    public List<SkillType> skillTypes;
    public Sprite icon;
    public int mpCost;
    public AnimationClip animation;
    public bool passiveSkill = false;
    public bool supportSkill = false;
    public bool move = false;
    public bool isAOE = false;
    public bool isWaitForCharge = false;
    public bool isUniqueSkill = false;

    public virtual void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        user.AttackState(animation);
        user.currentTarget = target;
        target.currentAttacker = user;
        user.currentMP -= mpCost;
    }

    public virtual void ApplyEffectOnEnd(CharacterRuntime user, CharacterRuntime target)
    {
    }

    public virtual void ApplyEffectOnFinishedAttack(CharacterRuntime user, CharacterRuntime target)
    {
    }

    public virtual void DoSpecialAction(CharacterRuntime user, CharacterRuntime target)
    {
    }

    public virtual bool CheckSkillCondition(CharacterRuntime user)
    {
        return true;
    }

    public virtual void OnFailCharge(CharacterRuntime user)
    {
    }

    public virtual int GetChargeTurn()
    {
        return 0;
    }

    public virtual List<StatusEffect> GetBuffsFromSkill(SkillBase skill)
    {
        List<StatusEffect> result = new();

        var type = skill.GetType();
        var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var field in fields)
        {
            if (typeof(StatusEffect).IsAssignableFrom(field.FieldType))
            {
                var effect = field.GetValue(skill) as StatusEffect;
                if (effect != null && effect.type == StatusType.Buff)
                {
                    result.Add(effect);
                }
            }
        }

        return result;
    }
}
