using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class SkillBase : ScriptableObject
{
    public int ID;
    public string skillName;
    public string description;
    public Sprite icon;
    public int mpCost;
    public AnimationClip animation;
    public bool passiveSkill = false;
    public bool supportSkill = false;
    public bool move = false;
    public bool isAOE = false;
    public bool isWaitForCharge = false;
    public bool isUniqueSkill = false;

    public virtual void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        user.AttackState(animation);
        user.currentTarget = target;
        target.currentAttacker = user;
        user.currentMP -= mpCost;
    }

    public virtual void ApplyEffectOnEnd(CharacterInBattle user, CharacterInBattle target)
    {
    }

    public virtual void ApplyEffectOnFinishedAttack(CharacterInBattle user, CharacterInBattle target)
    {
    }

    public virtual void DoSpecialAction(CharacterInBattle user, CharacterInBattle target)
    {
    }

    public virtual bool CheckSkillCondition(CharacterInBattle user, CharacterInBattle target)
    {
        return true;
    }

    public virtual void OnFailCharge(CharacterInBattle user, CharacterInBattle target)
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
