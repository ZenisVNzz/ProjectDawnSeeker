using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "test", story: "Check [MySelf] if Im Dead", category: "Conditions", id: "41fe6be20668f4dcb146d3fd9f5d12b8")]
public partial class TestCondition : Condition
{
    [SerializeReference] public BlackboardVariable<CharacterRuntime> MySelf;

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
