using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "ShouldIUseBuffSkill", story: "I Should use buff skill, check [self]", category: "Conditions", id: "f1414195b5be63cde27cf36851abb9e3")]
public partial class ShouldIUseBuffSkillCondition : Condition
{
    [SerializeReference] public BlackboardVariable<CharacterInBattle> Self;

    public override bool IsTrue()
    {
        SkillBase buffSkill = Self.Value.skillList.Find(skill => skill.passiveSkill);
        List<StatusEffect> buffs = buffSkill.GetBuffsFromSkill(buffSkill);
        foreach (StatusEffect effect in buffs)
        {
            if (Self.Value.activeStatusEffect.Exists(status => status.ID == effect.ID))
            {
                return false;
            }
        }    
        return true;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
