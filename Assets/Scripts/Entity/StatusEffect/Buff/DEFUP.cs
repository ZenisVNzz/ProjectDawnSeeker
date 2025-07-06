using UnityEngine;

[CreateAssetMenu(fileName = "NewDEFUP", menuName = "StatusEffect/DEFUP")]
public class DEFUP : StatusEffect
{
    public float PercentAmount;

    public override void OnApply(CharacterRuntime target)
    {
        target.IncreaseDEF(PercentAmount);
    }
    public override void OnTurn(CharacterRuntime target)
    {
    }
    public override void OnRemove(CharacterRuntime target)
    {
        target.DecreaseDEF(PercentAmount);
    }
}
