using UnityEngine;

[CreateAssetMenu(fileName = "Heals", menuName = "Skills/Priest/Heals")]
public class Heals : SkillBase
{
    public Heal heal;
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        heal.CasterATK = user.ATK;
        target.ApplyStatusEffect(heal, 1);
        user.savedHeal = user.ATK * 2f;
        base.DoAction(user, target);
    }
}
