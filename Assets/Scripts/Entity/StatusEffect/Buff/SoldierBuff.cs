using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SoldierBuff", menuName = "StatusEffect/SoldierBuff")]
public class SoldierBuff : StatusEffect
{
    public override void OnApply(CharacterInBattle target)
    {
        target.isGetATKBuffWhenDodge = true;
    }
    public override void OnTurn(CharacterInBattle target)
    {
    }
    public override void OnRemove(CharacterInBattle target)
    {
        target.isGetATKBuffWhenDodge = false;
        if (target.activeStatusEffect.Any(x => x.ID == 200023))
        {
            target.activeStatusEffect.RemoveAll(x => x.ID == 200023);
        }
    }
}