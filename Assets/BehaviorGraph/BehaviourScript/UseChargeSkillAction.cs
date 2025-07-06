using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UseChargeSkill", story: "I will use charge skill, assign [ChosenSkill] from [mySelf]", category: "Action", id: "9fe4e9c0b8e23efe8bb60481bec19fdf")]
public partial class UseChargeSkillAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterRuntime> MySelf;
    [SerializeReference] public BlackboardVariable<SkillBase> ChosenSkill;

    protected override Status OnStart()
    {
        SkillBase chargeSkill = MySelf.Value.skillList.Find(s => s.skillTypes.Contains(SkillType.Charge));
        ChosenSkill.Value = chargeSkill;
        return Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

