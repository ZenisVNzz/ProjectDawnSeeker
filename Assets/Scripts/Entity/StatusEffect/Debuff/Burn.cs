using UnityEngine;

[CreateAssetMenu(fileName = "NewBurn", menuName = "StatusEffect/Burn")]
public class Burn : StatusEffect
{
    public int PercentAmount;

    public override void OnApply(CharacterRuntime target)
    {
    }
    public override void OnTurn(CharacterRuntime target)
    {
        target.TakeDamagePercent(PercentAmount);
    }
    public override void OnRemove(CharacterRuntime target)
    {
    }
}
