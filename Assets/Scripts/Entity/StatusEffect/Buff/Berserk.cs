using UnityEngine;

[CreateAssetMenu(fileName = "NewBerserk", menuName = "StatusEffect/Berserk")]
public class Berserk : StatusEffect
{
    public float IncreaseATKPercentAmount;
    public float DecreaseDEFPercentAmount;

    public override void OnApply(CharacterInBattle target)
    {
        target.IncreaseATK(IncreaseATKPercentAmount);
        target.DecreaseDEF(DecreaseDEFPercentAmount);
    }
    public override void OnTurn(CharacterInBattle target)
    {
    }
    public override void OnRemove(CharacterInBattle target)
    {
        target.DecreaseATK(IncreaseATKPercentAmount);
        target.IncreaseDEF(DecreaseDEFPercentAmount);
    }
}
