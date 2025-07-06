using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "AmIAlive", story: "Am I still alive? Check [MySelf]", category: "Conditions", id: "eea5cb835fb906ea8b622563fe7ad98f")]
public partial class AmIAliveCondition : Condition
{
    [SerializeReference] public BlackboardVariable<CharacterRuntime> MySelf;

    public override bool IsTrue()
    {
        if (MySelf.Value.isAlive)
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
