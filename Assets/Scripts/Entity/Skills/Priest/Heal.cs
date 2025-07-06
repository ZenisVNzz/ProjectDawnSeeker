using UnityEngine;

[CreateAssetMenu(fileName = "Heals", menuName = "Skills/Priest/Heals")]
public class Heals : SkillBase
{
    public Heal heal;
    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        heal.CasterATK = user.ATK;
        user.savedHeal = user.ATK * 2f;
        target.ApplyStatusEffect(heal, 0);
        base.DoAction(user, target);
    }
}
