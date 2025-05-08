using UnityEngine;

[CreateAssetMenu(fileName = "NewManaRecovery", menuName = "StatusEffect/ManaRecovery")]
public class ManaRecovery : StatusEffect
{
    public int MPPercentAmount;
    public int DecreaseDEFPercentAmount;

    public override void OnApply(CharacterInBattle target)
    {
        target.DecreaseDEF(DecreaseDEFPercentAmount);
    }
    public override void OnTurn(CharacterInBattle target)
    {      
    }
    public override void OnRemove(CharacterInBattle target)
    {
        target.MPRecovery(MPPercentAmount);
        target.IncreaseDEF(DecreaseDEFPercentAmount);
    }
}
