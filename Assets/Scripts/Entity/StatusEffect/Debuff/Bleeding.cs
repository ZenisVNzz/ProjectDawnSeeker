using UnityEngine;

[CreateAssetMenu(fileName = "Bleeding", menuName = "StatusEffect/Bleeding")]
public class Bleeding : StatusEffect
{
    public override void OnApply(CharacterInBattle target)
    {
        target.isBleeding = true;
    }
    public override void OnTurn(CharacterInBattle target)
    {
    }
    public override void OnRemove(CharacterInBattle target)
    {
        target.isBleeding = false;
    }
}
