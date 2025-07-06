using System;
using System.Linq;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "AmIBeingProvoked", story: "[self] being provoked?", category: "Conditions", id: "6fcb7961a48aff0700e3672e62967ea8")]
public partial class AmIBeingProvokedCondition : Condition
{
    [SerializeReference] public BlackboardVariable<CharacterRuntime> Self;

    public override bool IsTrue()
    {
        if (Self.Value.activeStatusEffect.Any(e => e.ID == 200009))
        {
            return true;
        }
        return false;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
