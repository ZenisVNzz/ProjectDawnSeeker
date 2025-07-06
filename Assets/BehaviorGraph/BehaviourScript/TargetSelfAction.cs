using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "TargetSelf", story: "This is a skill that buff my self, so i assign [mySelf] as the [ChosenTarget]", category: "Action", id: "1bd0c0c7b55c901bbc9f9a28bdcc77b2")]
public partial class TargetSelfAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterRuntime> ChosenTarget;
    [SerializeReference] public BlackboardVariable<GameObject> mySelf;

    protected override Status OnStart()
    {
        CharacterRuntime character = mySelf.Value.GetComponent<CharacterRuntime>();
        ChosenTarget.Value = character;
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

