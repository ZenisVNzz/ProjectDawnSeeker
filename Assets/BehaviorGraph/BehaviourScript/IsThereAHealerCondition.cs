using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsThereAHealer", story: "There is a healer from [playerTeam]", category: "Conditions", id: "cf55a7cfa9d67fad51cf3d5b28315524")]
public partial class IsThereAHealerCondition : Condition
{
    [SerializeReference] public BlackboardVariable<List<GameObject>> PlayerTeam;

    public override bool IsTrue()
    {
        List<CharacterInBattle> playerTeamCharacters = PlayerTeam.Value.ConvertAll(player => player.GetComponent<CharacterInBattle>());
        playerTeamCharacters.RemoveAll(character => !character.isAlive);
        foreach (CharacterInBattle character in playerTeamCharacters)
        {
            if (character.characterTags.Any(tag => tag == Tags.Healer))
            {
                return true;
            }
            else
            {
                return false;
            }    
        }
        return false;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
