using UnityEngine;

[CreateAssetMenu(fileName = "NewCRUP", menuName = "StatusEffect/CRUP")]
public class CRUP : StatusEffect
{
    public float PercentAmount;

    public override void OnApply(CharacterInBattle target)
    {
        target.IncreaseCR(PercentAmount);
    }
    public override void OnTurn(CharacterInBattle target)
    {
    }
    public override void OnRemove(CharacterInBattle target)
    {
        target.DecreaseCR(PercentAmount);
    }
}
