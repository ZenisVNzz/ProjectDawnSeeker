using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "ArmoredAxeman_Skill4_Effect", menuName = "StatusEffect/Skill/ArmoredAxeman_Skill4_Effect")]
public class ArmoredAxeman_Skill4_Effect : StatusEffect
{
    public StatusEffect deadlyBlood;

    public override void OnApply(CharacterInBattle target)
    {
        target.ApplyStatusEffect(deadlyBlood, 99);
        target.isDealyBlood = true;
    }
    public override void OnTurn(CharacterInBattle target)
    {
    }
    public override void OnRemove(CharacterInBattle target)
    {
        target.isDealyBlood = false;

        var effectsToRemove = target.activeStatusEffect.Where(x => x.ID == 200027).ToList();

        foreach (var effect in effectsToRemove)
        {
            effect.OnRemove(target);
        }

        target.activeStatusEffect.RemoveAll(x => x.ID == 200027);
    }
}
