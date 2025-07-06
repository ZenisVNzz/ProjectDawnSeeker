using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UseNormalSkill", story: "I will use normal skill, assign [ChosenSkill] from [MySelf]", category: "Action", id: "859d106ff5e36f1b55289b4696925e0a")]
public partial class UseNormalSkillAction : Action
{
    [SerializeReference] public BlackboardVariable<SkillBase> ChosenSkill;
    [SerializeReference] public BlackboardVariable<CharacterInBattle> MySelf;

    protected override Status OnStart()
    {
        ChosenSkill.Value = MySelf.Value.skillList[0];
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

