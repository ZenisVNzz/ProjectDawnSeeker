using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Unity.VisualScripting;
using System.Collections;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WaitForTargetCounter", story: "[Im] waiting for [target] to finish countering", category: "Action", id: "c3bfdcad91fb0d5cd2138f8c7c99721f")]
public partial class WaitForTargetCounterAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterRuntime> Im;
    [SerializeReference] public BlackboardVariable<CharacterRuntime> Target;
    private bool attackerFinished = false;
    private bool targetFinished = false;

    protected override Status OnStart()
    {
        attackerFinished = false;
        targetFinished = false;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        var attacker = Im.Value;
        var target = Target.Value;

        if (!attackerFinished)
        {
            if (attacker.currentState == State.Idle)
            {
                target.Attack(target, attacker);
                attackerFinished = true;
            }
        }
        else if (!targetFinished)
        {
            if (target.currentState == State.Idle)
            {
                targetFinished = true;
                return Status.Success;
            }
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

