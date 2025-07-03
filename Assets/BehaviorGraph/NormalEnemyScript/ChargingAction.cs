using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Charging", story: "[I] charging this turn", category: "Action", id: "09ae07fe2b3e9e78e02fd5910a8812e7")]
public partial class ChargingAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterInBattle> I;

    protected override Status OnStart()
    {
        I.Value.chargeTurn++;
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

