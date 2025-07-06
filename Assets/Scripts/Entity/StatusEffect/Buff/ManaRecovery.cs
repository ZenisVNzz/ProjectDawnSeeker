using UnityEngine;

[CreateAssetMenu(fileName = "NewManaRecovery", menuName = "StatusEffect/ManaRecovery")]
public class ManaRecovery : StatusEffect
{
    public int MPPercentAmount;

    public override void OnApply(CharacterRuntime target)
    {
        target.MPRecovery(MPPercentAmount);
    }
    public override void OnTurn(CharacterRuntime target)
    {      
    }
    public override void OnRemove(CharacterRuntime target)
    {
    }
}
