using UnityEngine;

[CreateAssetMenu(fileName = "Curse", menuName = "StatusEffect/Curse")]
public class Curse : StatusEffect
{
    public override void OnApply(CharacterInBattle target)
    {
        target.isMPRecoveryAble = false;
    }
    public override void OnTurn(CharacterInBattle target)
    {
    }
    public override void OnRemove(CharacterInBattle target)
    {
        target.isMPRecoveryAble = true;
    }
}
