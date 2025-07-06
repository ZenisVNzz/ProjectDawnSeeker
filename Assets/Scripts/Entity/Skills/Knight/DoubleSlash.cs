using UnityEngine;

[CreateAssetMenu(fileName = "DoubleSlash", menuName = "Skills/Knight/DoubleSlash")]
public class DoubleSlash : SkillBase
{
    public StatusEffect bleeding;
    public StatusEffect atkUP;
    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK * 1.3f, 2, user, target);
        base.DoAction(user, target);     
    }

    public override void ApplyEffectOnEnd(CharacterRuntime user, CharacterRuntime target)
    {
        user.ApplyStatusEffect(atkUP, 2);
    }
}
