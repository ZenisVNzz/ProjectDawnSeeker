using UnityEngine;

[CreateAssetMenu(fileName = "DoubleSlash", menuName = "Skills/Knight/DoubleSlash")]
public class DoubleSlash : SkillBase
{
    public StatusEffect bleeding;
    public StatusEffect atkUP;
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 0.6f, user, target);
        target.TakeDamage(user.ATK * 0.6f, user, target);
        target.ApplyStatusEffect(bleeding, 2);
        user.ApplyStatusEffect(atkUP, 2);
    }
}
