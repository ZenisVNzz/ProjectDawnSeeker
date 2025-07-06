using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AssignMySelf", story: "Assign [MySelf] as [Target]", category: "Action", id: "285d0f265f6f2e00b01119f2f0c8507e")]
public partial class AssignMySelfAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterInBattle> MySelf;
    [SerializeReference] public BlackboardVariable<CharacterInBattle> Target;

    protected override Status OnStart()
    {
        Target.Value = MySelf.Value;
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

