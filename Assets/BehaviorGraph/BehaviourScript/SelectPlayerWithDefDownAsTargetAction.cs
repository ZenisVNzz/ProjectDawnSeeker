using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Linq;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SelectPlayerWithDefDownAsTarget", story: "Select Player with DefDown as Target, assign [ChoosenTarget] from [PlayerTeam]", category: "Action", id: "b9b80e1907d692a8f7aa7466a4225482")]
public partial class SelectPlayerWithDefDownAsTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterRuntime> ChoosenTarget;
    [SerializeReference] public BlackboardVariable<List<GameObject>> PlayerTeam;

    protected override Status OnStart()
    {
        List<CharacterRuntime> playerTeamCharacters = PlayerTeam.Value.ConvertAll(player => player.GetComponent<CharacterRuntime>());
        playerTeamCharacters.RemoveAll(character => !character.isAlive);
        foreach (CharacterRuntime character in playerTeamCharacters)
        {
            if (character.activeStatusEffect.Any(e => e.ID == 200011))
            {
                ChoosenTarget.Value = character;
                return Status.Success;
            }
        }
        return Status.Failure;
    }

    protected override Status OnUpdate()
    {
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

