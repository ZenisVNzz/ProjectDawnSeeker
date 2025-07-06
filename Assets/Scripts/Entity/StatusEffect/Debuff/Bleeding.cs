using UnityEngine;

[CreateAssetMenu(fileName = "Bleeding", menuName = "StatusEffect/Bleeding")]
public class Bleeding : StatusEffect
{
    public override void OnApply(CharacterRuntime target)
    {
        target.isBleeding = true;
    }
    public override void OnTurn(CharacterRuntime target)
    {
    }
    public override void OnRemove(CharacterRuntime target)
    {
        target.isBleeding = false;
    }
}
