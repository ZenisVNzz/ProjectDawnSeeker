using UnityEngine;

[CreateAssetMenu(fileName = "DeepWound", menuName = "StatusEffect/DeepWound")]
public class DeepWound : StatusEffect
{
    public override void OnApply(CharacterRuntime target)
    {
        target.isDeepWound = true;
    }
    public override void OnTurn(CharacterRuntime target)
    {
    }
    public override void OnRemove(CharacterRuntime target)
    {
        target.isDeepWound = false;
    }
}
