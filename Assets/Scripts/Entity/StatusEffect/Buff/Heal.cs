using UnityEngine;

[CreateAssetMenu(fileName = "NewHeal", menuName = "StatusEffect/Heal")]
public class Heal : StatusEffect
{
    public int CasterATK;
    public int percent;

    public override void OnApply(CharacterInBattle target)
    {
        int Amount = CasterATK * (percent / 100);  
        target.Heal(Amount);
    }
    public override void OnTurn(CharacterInBattle target)
    {
    }
    public override void OnRemove(CharacterInBattle target)
    {
    }
}
