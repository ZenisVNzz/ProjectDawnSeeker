using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AddActionToTheUI", story: "Caster is [mySelf] - Skill is [ChosenSkill] - Target is [ChosenTarget] - Add all of this to the [ActionOrder] to display on the UI", category: "Action", id: "4862e9b7a95772126453c6b9d14c51c7")]
public partial class AddActionToTheUiAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterInBattle> MySelf;
    [SerializeReference] public BlackboardVariable<SkillBase> ChosenSkill;
    [SerializeReference] public BlackboardVariable<CharacterInBattle> ChosenTarget;
    [SerializeReference] public BlackboardVariable<ActionOrder> ActionOrder;

    protected override Status OnStart()
    {
        bool isTargetAlly = ChosenSkill.Value.supportSkill;

        ActionOrder.Value.AddAction(
            MySelf.Value,
            ChosenTarget.Value,
            ChosenSkill.Value,
            isTargetAlly
        );

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

