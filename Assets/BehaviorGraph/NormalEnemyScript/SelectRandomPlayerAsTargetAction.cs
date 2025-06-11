using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Linq;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SelectRandomPlayerAsTarget", story: "Select random Player as Target, assign [ChoosenTarget] from [PlayerTeam]", category: "Action", id: "5880229a79ace10869d1bdbc329fdfbd")]
public partial class SelectRandomPlayerAsTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterInBattle> ChoosenTarget;
    [SerializeReference] public BlackboardVariable<List<GameObject>> PlayerTeam;

    protected override Status OnStart()
    {
        List<CharacterInBattle> playerTeamCharacters = PlayerTeam.Value.ConvertAll(player => player.GetComponent<CharacterInBattle>());
        playerTeamCharacters.RemoveAll(character => !character.isAlive);
        ChoosenTarget.Value = GetRandomAlive(playerTeamCharacters);

        return Status.Success;
    }

    public CharacterInBattle GetRandomAlive(List<CharacterInBattle> list)
    {
        var aliveList = list.Where(x => x.isAlive).ToList();
        if (aliveList.Count == 0) return null;

        int index = UnityEngine.Random.Range(0, aliveList.Count);
        return aliveList[index];
    }

    protected override Status OnUpdate()
    {
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

