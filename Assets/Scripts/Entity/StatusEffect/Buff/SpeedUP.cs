using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeedUP", menuName = "StatusEffect/SpeedUP")]
public class SpeedUP : StatusEffect
{
    public int PercentAmount;

    public override void OnApply(CharacterInBattle target)
    {
        target.IncreaseDC(PercentAmount);
    }
    public override void OnTurn(CharacterInBattle target)
    {
    }
    public override void OnRemove(CharacterInBattle target)
    {
        target.DecreaseDC(PercentAmount);
    }
}
