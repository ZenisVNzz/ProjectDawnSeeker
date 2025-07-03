using UnityEngine;

[CreateAssetMenu(fileName = "NewHeal", menuName = "StatusEffect/Heal")]
public class Heal : StatusEffect
{
    public float CasterATK;
    public float percent;

    public override void OnApply(CharacterInBattle target)
    {
        float Amount = CasterATK * (percent / 100f);  
        target.Heal(Amount, false);
    }
    public override void OnTurn(CharacterInBattle target)
    {
    }
    public override void OnRemove(CharacterInBattle target)
    {
    }
}
