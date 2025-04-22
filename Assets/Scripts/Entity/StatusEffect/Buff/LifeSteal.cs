using UnityEngine;

[CreateAssetMenu(fileName = "NewLifeSteal", menuName = "StatusEffect/LifeSteal")]
public class LifeSteal : StatusEffect
{
    public override void OnApply(CharacterInBattle target)
    {
        target.isLifeSteal = true;
    }
    public override void OnTurn(CharacterInBattle target)
    {
    }
    public override void OnRemove(CharacterInBattle target)
    {
        target.isLifeSteal = false;
    }
}
