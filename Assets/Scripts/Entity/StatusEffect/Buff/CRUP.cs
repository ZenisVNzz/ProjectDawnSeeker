using UnityEngine;

[CreateAssetMenu(fileName = "NewCRUP", menuName = "StatusEffect/CRUP")]
public class CRUP : StatusEffect
{
    public float PercentAmount;

    public override void OnApply(CharacterRuntime target)
    {
        target.IncreaseCR(PercentAmount);
    }
    public override void OnTurn(CharacterRuntime target)
    {
    }
    public override void OnRemove(CharacterRuntime target)
    {
        target.DecreaseCR(PercentAmount);
    }
}
