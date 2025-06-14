using UnityEngine;

[CreateAssetMenu(fileName = "NewManaRecovery", menuName = "StatusEffect/ManaRecovery")]
public class ManaRecovery : StatusEffect
{
    public int MPPercentAmount;

    public override void OnApply(CharacterInBattle target)
    {
        target.MPRecovery(MPPercentAmount);
    }
    public override void OnTurn(CharacterInBattle target)
    {      
    }
    public override void OnRemove(CharacterInBattle target)
    {
    }
}
