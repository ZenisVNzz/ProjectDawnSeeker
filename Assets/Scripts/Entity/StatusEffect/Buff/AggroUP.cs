using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "AggroUP", menuName = "StatusEffect/AggroUP")]
public class AggroUP : StatusEffect
{
    public override void OnApply(CharacterInBattle target)
    {
        target.isAggroUp = true;
    }
    public override void OnTurn(CharacterInBattle target)
    {
    }
    public override void OnRemove(CharacterInBattle target)
    {
        target.isAggroUp = false;
        if (target.activeStatusEffect.Any(x => x.ID == 200026))
        {
            target.activeStatusEffect.RemoveAll(x => x.ID == 200026);
        }
    }
}
