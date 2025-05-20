using UnityEngine;

[CreateAssetMenu(fileName = "Paralysis", menuName = "StatusEffect/Paralysis")]
public class Paralysis : StatusEffect
{
    public override void OnApply(CharacterInBattle target)
    {
        target.isActionAble = false;
    }
    public override void OnTurn(CharacterInBattle target)
    {
    }
    public override void OnRemove(CharacterInBattle target)
    {
        target.isActionAble = true;
    }
}
