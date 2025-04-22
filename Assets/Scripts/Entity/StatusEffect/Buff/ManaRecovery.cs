using UnityEngine;

public class ManaRecovery : StatusEffect
{
    public int MPPercentAmount;
    public int DecreaseDEFPercentAmount;

    public override void OnApply(CharacterInBattle target)
    {
        target.MPRecovery(MPPercentAmount);
        target.DecreaseDEF(DecreaseDEFPercentAmount);
    }
    public override void OnTurn(CharacterInBattle target)
    {
    }
    public override void OnRemove(CharacterInBattle target)
    {
        target.IncreaseDEF(DecreaseDEFPercentAmount);
    }
}
