using UnityEngine;

[CreateAssetMenu(fileName = "NewBurn", menuName = "StatusEffect/Burn")]
public class Burn : StatusEffect
{
    public int PercentAmount;

    public override void OnApply(CharacterInBattle target)
    {
    }
    public override void OnTurn(CharacterInBattle target)
    {
        target.TakeDamagePercent(PercentAmount);
    }
    public override void OnRemove(CharacterInBattle target)
    {
    }
}
