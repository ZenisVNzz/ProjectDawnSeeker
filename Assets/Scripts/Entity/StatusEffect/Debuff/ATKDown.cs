using UnityEngine;

[CreateAssetMenu(fileName = "NewATKDown", menuName = "StatusEffect/ATKDown")]
public class ATKDown : StatusEffect
{
    public float PercentAmount;

    public override void OnApply(CharacterRuntime target)
    {
        target.DecreaseATK(PercentAmount);
    }
    public override void OnTurn(CharacterRuntime target)
    {
    }
    public override void OnRemove(CharacterRuntime target)
    {
        target.IncreaseATK(PercentAmount);
    }
}
