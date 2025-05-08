using UnityEngine;

[CreateAssetMenu(fileName = "HolyLight", menuName = "Skills/Priest/HolyLight")]
public class HolyLight : SkillBase
{
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 1.2f, user, target);
        user.savedDmg = user.ATK * 1.2f - target.DEF;
        base.DoAction(user, target);
    }
}
