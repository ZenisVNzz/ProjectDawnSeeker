using UnityEngine;

[CreateAssetMenu(fileName = "Fear", menuName = "StatusEffect/Fear")]
public class Fear : StatusEffect
{
    public float PercentAmount;

    public override void OnApply(CharacterInBattle target)
    {
        target.DecreaseATK(PercentAmount);
        target.DecreaseDEF(PercentAmount);
        target.DecreaseCR(PercentAmount);
        target.DecreaseCD(PercentAmount);
        target.DecreaseDC(PercentAmount);
        target.DecreasePC(PercentAmount);
    }
    public override void OnTurn(CharacterInBattle target)
    {
    }
    public override void OnRemove(CharacterInBattle target)
    {
        target.IncreaseATK(PercentAmount);
        target.IncreaseDEF(PercentAmount);
        target.IncreaseCR(PercentAmount);
        target.IncreaseCD(PercentAmount);
        target.IncreaseDC(PercentAmount);
        target.IncreasePC(PercentAmount);
    }
}
