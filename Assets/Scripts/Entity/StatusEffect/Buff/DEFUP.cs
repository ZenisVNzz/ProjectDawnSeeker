using UnityEngine;

[CreateAssetMenu(fileName = "NewDEFUP", menuName = "StatusEffect/DEFUP")]
public class DEFUP : StatusEffect
{
    public int PercentAmount;

    public override void OnApply(CharacterInBattle target)
    {
        target.IncreaseDEF(PercentAmount);
    }
    public override void OnTurn(CharacterInBattle target)
    {
    }
    public override void OnRemove(CharacterInBattle target)
    {
        target.DecreaseDEF(PercentAmount);
    }
}
