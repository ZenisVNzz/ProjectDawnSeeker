using UnityEngine;

[CreateAssetMenu(fileName = "NewATKUP", menuName = "StatusEffect/ATKUP")]
public class ATKUP : StatusEffect
{
    public float PercentAmount;

    public override void OnApply(CharacterInBattle target)
    {
        target.IncreaseATK(PercentAmount);
    }
    public override void OnTurn(CharacterInBattle target)
    {
    }
    public override void OnRemove(CharacterInBattle target)
    {
        target.DecreaseATK(PercentAmount);
    }
}
