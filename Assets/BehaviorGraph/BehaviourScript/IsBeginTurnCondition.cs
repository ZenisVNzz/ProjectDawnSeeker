using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsBeginTurn", story: "Agent check if [turn]", category: "Conditions", id: "40c7673821982b9569eccaa37bf3de2e")]
public partial class IsBeginTurnCondition : Condition
{
    [SerializeReference] public BlackboardVariable<bool> Turn;

    public override bool IsTrue()
    {
        return true;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
