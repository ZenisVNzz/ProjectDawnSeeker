using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SoldierBuff", menuName = "StatusEffect/SoldierBuff")]
public class SoldierBuff : StatusEffect
{
    public override void OnApply(CharacterRuntime target)
    {
        target.isGetATKBuffWhenDodge = true;
    }
    public override void OnTurn(CharacterRuntime target)
    {
    }
    public override void OnRemove(CharacterRuntime target)
    {
        target.isGetATKBuffWhenDodge = false;

        var effectsToRemove = target.activeStatusEffect.Where(x => x.ID == 200023).ToList();

        foreach (var effect in effectsToRemove)
        {
            effect.OnRemove(target);
        }

        target.activeStatusEffect.RemoveAll(x => x.ID == 200023);
    }
}