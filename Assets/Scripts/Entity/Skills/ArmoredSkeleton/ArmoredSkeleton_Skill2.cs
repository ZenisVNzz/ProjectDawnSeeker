using UnityEngine;

[CreateAssetMenu(fileName = "ArmoredSkeleton_Skill2", menuName = "Skills/ArmoredSkeleton/ArmoredSkeleton_Skill2")]
public class ArmoredSkeleton_Skill2 : SkillBase
{
	public StatusEffect atkDown;

	public override void DoAction(CharacterRuntime user, CharacterRuntime target)
	{
		target.TakeDamage(user.ATK * 1.1f, 4, user, target);
		target.isPenetrating = true;
		base.DoAction(user, target);
	}

    public override void ApplyEffectOnFinishedAttack(CharacterRuntime user, CharacterRuntime target)
    {
        target.ApplyStatusEffect(atkDown, 1);
    }
}