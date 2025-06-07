using UnityEngine;

[CreateAssetMenu(fileName = "Skeleton_Skill2", menuName = "Skills/Skeleton/Skeleton_Skill2")]
public class Skeleton_Skill2 : SkillBase
{
    public StatusEffect bleed;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 1.15f, 1, user, target);
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnFinishedAttack(CharacterInBattle user, CharacterInBattle target)
    {
        target.ApplyStatusEffect(bleed, 1);
    }
}