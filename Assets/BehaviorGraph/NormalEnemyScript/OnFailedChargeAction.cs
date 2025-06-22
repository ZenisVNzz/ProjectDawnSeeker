using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "OnFailedCharge", story: "[I] failed to charge", category: "Action", id: "c289ab2af408d21b969fb85e4eea1c44")]
public partial class OnFailedChargeAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterInBattle> I;

    protected override Status OnStart()
    {
        SkillBase chargeSKill = I.Value.skillList.Find(s => s.skillTypes.Contains(SkillType.Charge));
        chargeSKill.OnFailCharge(I.Value);
        I.Value.isCharge = false;
        return Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

