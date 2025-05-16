using UnityEngine;

[CreateAssetMenu(fileName = "NewDEFDown", menuName = "StatusEffect/DEFDown")]
public class DEFDown : StatusEffect
{
    public float PercentAmount;

    public override void OnApply(CharacterInBattle target)
    {
        target.DecreaseDEF(PercentAmount);
    }
    public override void OnTurn(CharacterInBattle target)
    {
    }
    public override void OnRemove(CharacterInBattle target)
    {
        target.IncreaseDEF(PercentAmount);
    }
}
