using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "ImDead", story: "Check [mySelf] if I'm dead", category: "Conditions", id: "b36d76bdbbf393147710a67f14129dcd")]
public partial class ImDeadCondition : Condition
{
    [SerializeReference] public BlackboardVariable<CharacterInBattle> MySelf;

    public override bool IsTrue()
    {
        if (!MySelf.Value.isAlive)
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
