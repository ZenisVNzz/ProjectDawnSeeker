using UnityEngine;

[CreateAssetMenu(fileName = "DoubleSlash", menuName = "Skills/Knight/DoubleSlash")]
public class DoubleSlash : SkillBase
{
    public StatusEffect bleeding;
    public StatusEffect atkUP;
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 1.2f, 2, user, target);
        base.DoAction(user, target);     
    }

    public override void ApplyEffectOnEnd(CharacterInBattle user, CharacterInBattle target)
    {
        user.ApplyStatusEffect(atkUP, 2);
    }
}
