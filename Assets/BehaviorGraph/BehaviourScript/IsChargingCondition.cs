using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsCharging", story: "[Im] charging", category: "Conditions", id: "219c49e51543e7673868c89035c33121")]
public partial class IsChargingCondition : Condition
{
    [SerializeReference] public BlackboardVariable<CharacterInBattle> Im;

    public override bool IsTrue()
    {
        if (Im.Value.isCharge)
        {
            return true;
        }  
        else
        {
            return false;
        }    
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
