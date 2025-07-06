using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "TargetIsCounter", story: "Check [ChosenTarget] if Target countered the attack", category: "Conditions", id: "38c34a9fbc1645651056d7bd80218bce")]
public partial class TargetIsCounterCondition : Condition
{
    [SerializeReference] public BlackboardVariable<CharacterInBattle> ChosenTarget;

    public override bool IsTrue()
    {
        if (ChosenTarget.Value.isParry)
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
