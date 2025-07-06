using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ResetStatus", story: "Reset status of [mySelf] and [ChosenTarget]", category: "Action", id: "0767ca5cb0ffaa47f0836c0151b07131")]
public partial class ResetStatusAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterRuntime> MySelf;
    [SerializeReference] public BlackboardVariable<CharacterRuntime> ChosenTarget;

    protected override Status OnStart()
    {
        MySelf.Value.ResetState();
        ChosenTarget.Value.ResetState();  
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

