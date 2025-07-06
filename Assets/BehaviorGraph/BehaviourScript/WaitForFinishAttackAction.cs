using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WaitForFinishAttack", story: "Wait until [i] finish action", category: "Action", id: "05ede25d5b78d00b637deb95f04924cd")]
public partial class WaitForFinishAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterInBattle> I;

    protected override Status OnStart()
    {
        if (I.Value.currentState != State.Idle)
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

