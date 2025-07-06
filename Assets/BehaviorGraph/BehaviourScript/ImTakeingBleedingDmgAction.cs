using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ImTakeingBleedingDmg", story: "[Im] take bleeding damage", category: "Action", id: "30174b6e2f8203943dd5f7b64771c5fd")]
public partial class ImTakeingBleedingDmgAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterRuntime> Im;

    protected override Status OnStart()
    {
        Im.Value.TakeBleedingDamage(Im.Value.ATK * 0.15f);
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

