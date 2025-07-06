using UnityEngine;

[CreateAssetMenu(fileName = "NewATKUP", menuName = "StatusEffect/ATKUP")]
public class ATKUP : StatusEffect
{
    public float PercentAmount;

    public override void OnApply(CharacterRuntime target)
    {
        target.IncreaseATK(PercentAmount);
    }
    public override void OnTurn(CharacterRuntime target)
    {
    }
    public override void OnRemove(CharacterRuntime target)
    {
        target.DecreaseATK(PercentAmount);
    }
}
