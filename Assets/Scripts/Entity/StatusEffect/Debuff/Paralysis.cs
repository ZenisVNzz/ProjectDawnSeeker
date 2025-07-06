using UnityEngine;

[CreateAssetMenu(fileName = "Paralysis", menuName = "StatusEffect/Paralysis")]
public class Paralysis : StatusEffect
{
    public override void OnApply(CharacterRuntime target)
    {
        target.isActionAble = false;
    }
    public override void OnTurn(CharacterRuntime target)
    {
    }
    public override void OnRemove(CharacterRuntime target)
    {
        target.isActionAble = true;
    }
}
