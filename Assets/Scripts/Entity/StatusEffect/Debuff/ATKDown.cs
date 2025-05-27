using UnityEngine;

[CreateAssetMenu(fileName = "NewATKDown", menuName = "StatusEffect/ATKDown")]
public class ATKDown : StatusEffect
{
    public int PercentAmount;

    public override void OnApply(CharacterInBattle target)
    {
        target.DecreaseATK(PercentAmount);
    }
    public override void OnTurn(CharacterInBattle target)
    {
    }
    public override void OnRemove(CharacterInBattle target)
    {
        target.IncreaseATK(PercentAmount);
    }
}
