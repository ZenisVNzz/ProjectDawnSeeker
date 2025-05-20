using UnityEngine;

[CreateAssetMenu(fileName = "DeepWound", menuName = "StatusEffect/DeepWound")]
public class DeepWound : StatusEffect
{
    public override void OnApply(CharacterInBattle target)
    {
        target.isDeepWound = true;
    }
    public override void OnTurn(CharacterInBattle target)
    {
    }
    public override void OnRemove(CharacterInBattle target)
    {
        target.isDeepWound = false;
    }
}
