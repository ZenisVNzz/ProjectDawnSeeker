using UnityEngine;

[CreateAssetMenu(fileName = "NewDEFDown", menuName = "StatusEffect/DEFDown")]
public class DEFDown : StatusEffect
{
    public float PercentAmount;

    public override void OnApply(CharacterRuntime target)
    {
        target.DecreaseDEF(PercentAmount);
    }
    public override void OnTurn(CharacterRuntime target)
    {
    }
    public override void OnRemove(CharacterRuntime target)
    {
        target.IncreaseDEF(PercentAmount);
    }
}
