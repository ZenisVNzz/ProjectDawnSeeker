using System;
using System.Linq;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "ShouldIUseStrongSkill", story: "I should use strong skill, check [ChosenTarget] and [MySelf]", category: "Conditions", id: "e1b83d24b04a394a3951ba0898ff2da3")]
public partial class ShouldIUseStrongSkillCondition : Condition
{
    [SerializeReference] public BlackboardVariable<CharacterRuntime> ChosenTarget;
    [SerializeReference] public BlackboardVariable<CharacterRuntime> MySelf;

    public override bool IsTrue()
    {
        if (MySelf.Value.activeStatusEffect.Any(e => e.ID == 200010))
        {
            return false;
        }
        if (ChosenTarget.Value.currentHP <= ChosenTarget.Value.HP * 0.1f)
        {
            return false;
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
