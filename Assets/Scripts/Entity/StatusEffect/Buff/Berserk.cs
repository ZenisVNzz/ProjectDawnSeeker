using UnityEngine;

[CreateAssetMenu(fileName = "NewBerserk", menuName = "StatusEffect/Berserk")]
public class Berserk : StatusEffect
{
    public float IncreaseATKPercentAmount;
    public float DecreaseDEFPercentAmount;

    public override void OnApply(CharacterRuntime target)
    {
        target.IncreaseATK(IncreaseATKPercentAmount);
        target.DecreaseDEF(DecreaseDEFPercentAmount);
    }
    public override void OnTurn(CharacterRuntime target)
    {
    }
    public override void OnRemove(CharacterRuntime target)
    {
        target.DecreaseATK(IncreaseATKPercentAmount);
        target.IncreaseDEF(DecreaseDEFPercentAmount);
    }
}
