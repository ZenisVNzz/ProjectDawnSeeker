using UnityEngine;

[CreateAssetMenu(fileName = "Curse", menuName = "StatusEffect/Curse")]
public class Curse : StatusEffect
{
    public override void OnApply(CharacterRuntime target)
    {
        target.isMPRecoveryAble = false;
    }
    public override void OnTurn(CharacterRuntime target)
    {
    }
    public override void OnRemove(CharacterRuntime target)
    {
        target.isMPRecoveryAble = true;
    }
}
