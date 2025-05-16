using UnityEngine;

[CreateAssetMenu(fileName = "NewBloodthirsty", menuName = "StatusEffect/Bloodthirsty")]
public class Bloodthirsty : StatusEffect
{
    public override void OnApply(CharacterInBattle target)
    {
        target.IncreaseATK(2.5f);
        target.IncreaseDEF(2.5f);
        target.IncreaseCR(2.5f);
    }
    public override void OnTurn(CharacterInBattle target)
    {
    }
    public override void OnRemove(CharacterInBattle target)
    {
        target.DecreaseATK(2.5f);
        target.DecreaseDEF(2.5f);
        target.DecreaseCR(2.5f);
    }
}
