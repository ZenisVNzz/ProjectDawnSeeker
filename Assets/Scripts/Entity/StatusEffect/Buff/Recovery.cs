using UnityEngine;

[CreateAssetMenu(fileName = "Recovery", menuName = "StatusEffect/Recovery")]
public class Recovery : StatusEffect
{
    public float CasterATK;
    public float percent;

    public override void OnApply(CharacterRuntime target)
    {
        float Amount = CasterATK * (percent / 100f);
        target.Heal(Amount, true);
    }
    public override void OnTurn(CharacterRuntime target)
    {
        float Amount = CasterATK * (percent / 100f);
        target.Heal(Amount, true);
    }
    public override void OnRemove(CharacterRuntime target)
    {
    }
}
