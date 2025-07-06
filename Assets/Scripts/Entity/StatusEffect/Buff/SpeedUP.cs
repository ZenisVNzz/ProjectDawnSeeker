using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeedUP", menuName = "StatusEffect/SpeedUP")]
public class SpeedUP : StatusEffect
{
    public float PercentAmount;

    public override void OnApply(CharacterRuntime target)
    {
        target.IncreaseDC(PercentAmount);
    }
    public override void OnTurn(CharacterRuntime target)
    {
    }
    public override void OnRemove(CharacterRuntime target)
    {
        target.DecreaseDC(PercentAmount);
    }
}
