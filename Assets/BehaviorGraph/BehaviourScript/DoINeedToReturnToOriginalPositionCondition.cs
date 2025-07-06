using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "DoINeedToReturnToOriginalPosition", story: "I need to return to original position, check [OriginalPosition] and [MySelf]", category: "Conditions", id: "29d710fed70ca8969a18f62c55c30931")]
public partial class DoINeedToReturnToOriginalPositionCondition : Condition
{
    [SerializeReference] public BlackboardVariable<CharacterRuntime> MySelf;
    [SerializeReference] public BlackboardVariable<Vector3> OriginalPosition;

    public override bool IsTrue()
    {
        if (MySelf.Value.transform.position != OriginalPosition.Value)
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
