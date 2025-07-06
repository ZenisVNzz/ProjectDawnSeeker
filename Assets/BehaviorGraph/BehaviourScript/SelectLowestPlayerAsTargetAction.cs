using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Linq;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SelectLowestPlayerAsTarget", story: "Select lowest player as target, assign [ChoosenTarget] from [PlayerTeam]", category: "Action", id: "37adf52a155767ce0bf8eb1642ee319d")]
public partial class SelectLowestPlayerAsTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterRuntime> ChoosenTarget;
    [SerializeReference] public BlackboardVariable<List<GameObject>> PlayerTeam;

    protected override Status OnStart()
    {
        List<CharacterRuntime> playerTeamCharacters = PlayerTeam.Value.ConvertAll(player => player.GetComponent<CharacterRuntime>());
        playerTeamCharacters.RemoveAll(character => !character.isAlive);

        CharacterRuntime lowestHPCharacter = playerTeamCharacters.OrderBy(character => character.currentHP).FirstOrDefault();
        ChoosenTarget.Value = lowestHPCharacter;
        if (lowestHPCharacter == null)
        {
            return Status.Failure;
        }
        else
        {
            return Status.Success;
        }         
    }

    protected override Status OnUpdate()
    {
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

