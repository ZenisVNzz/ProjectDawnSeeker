using UnityEngine;

[CreateAssetMenu(fileName = "NewHeal", menuName = "StatusEffect/Heal")]
public class Heal : StatusEffect
{
    public int Amount;

    public override void OnApply(CharacterInBattle target)
    {
        target.Heal(Amount);
    }
    public override void OnTurn(CharacterInBattle target)
    {
    }
    public override void OnRemove(CharacterInBattle target)
    {
    }
}
