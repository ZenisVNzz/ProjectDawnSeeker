using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RemoveActionOrder", story: "Remove action in the UI from [ActionOrder] where Caster is [mySelf] and Skill is [ChosenSkill]", category: "Action", id: "0b6c66b2afd57913716bd3045c94c988")]
public partial class RemoveActionOrderAction : Action
{
    [SerializeReference] public BlackboardVariable<ActionOrder> ActionOrder;
    [SerializeReference] public BlackboardVariable<CharacterInBattle> MySelf;
    [SerializeReference] public BlackboardVariable<SkillBase> ChosenSkill;

    protected override Status OnStart()
    {
        ActionOrder.Value.RemoveAction(MySelf.Value, ChosenSkill.Value);
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

