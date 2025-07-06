using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IfImBleeding", story: "[Im] bleeding?", category: "Conditions", id: "93747afc1cf16f81bf03d689a26f3fd8")]
public partial class IfImBleedingCondition : Condition
{
    [SerializeReference] public BlackboardVariable<CharacterRuntime> Im;

    public override bool IsTrue()
    {
        if (Im.Value.isBleeding)
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
