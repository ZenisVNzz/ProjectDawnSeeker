using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Unity.VisualScripting;
using System.Linq;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChosePlayerWithHighestDef", story: "Select player with highest def from [playerTeam] as [target]", category: "Action", id: "ca09dfd33b8bca9f71d8805d0a9030b2")]
public partial class ChosePlayerWithHighestDefAction : Action
{
    [SerializeReference] public BlackboardVariable<List<GameObject>> PlayerTeam;
    [SerializeReference] public BlackboardVariable<CharacterInBattle> Target;

    protected override Status OnStart()
    {
        List<CharacterInBattle> playerTeam = PlayerTeam.Value.ConvertAll(c => c.GetComponent<CharacterInBattle>());
        playerTeam.RemoveAll(c => !c.isAlive);
        CharacterInBattle HighestDefPlayer = playerTeam.OrderByDescending(c => c.DEF).FirstOrDefault();
        Target.Value = HighestDefPlayer;
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

