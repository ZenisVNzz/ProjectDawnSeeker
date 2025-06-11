using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChooseBuffSkill", story: "I will use buff skill, assign [choosenSkill] from [self]", category: "Action", id: "e46b3f6d2f235fe5d8ce75e8ec4563a6")]
public partial class ChooseBuffSkillAction : Action
{
    [SerializeReference] public BlackboardVariable<SkillBase> ChoosenSkill;
    [SerializeReference] public BlackboardVariable<CharacterInBattle> Self;

    protected override Status OnStart()
    {
        SkillBase buffSkill = Self.Value.skillList.Find(skill => skill.passiveSkill);
        ChoosenSkill.Value = buffSkill;
        if (ChoosenSkill.Value == null || buffSkill.mpCost > Self.Value.currentMP)
        {
            return Status.Failure;
        }
        else
        {
            return Status.Success;
        } 
    }

    protected override Status OnUpdate()
    {
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

