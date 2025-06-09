using UnityEngine;

[CreateAssetMenu(fileName = "Recovery", menuName = "StatusEffect/Recovery")]
public class Recovery : StatusEffect
{
    public float CasterATK;
    public float percent;

    public override void OnApply(CharacterInBattle target)
    {
        float Amount = CasterATK * (percent / 100f);
        target.Heal(Amount);
    }
    public override void OnTurn(CharacterInBattle target)
    {
        float Amount = CasterATK * (percent / 100f);
        target.Heal(Amount);
    }
    public override void OnRemove(CharacterInBattle target)
    {
    }
}
