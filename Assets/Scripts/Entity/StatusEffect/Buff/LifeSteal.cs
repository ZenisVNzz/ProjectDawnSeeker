using UnityEngine;

[CreateAssetMenu(fileName = "NewLifeSteal", menuName = "StatusEffect/LifeSteal")]
public class LifeSteal : StatusEffect
{
    public override void OnApply(CharacterRuntime target)
    {
        target.isLifeSteal = true;
    }
    public override void OnTurn(CharacterRuntime target)
    {
    }
    public override void OnRemove(CharacterRuntime target)
    {
        target.isLifeSteal = false;
    }
}
