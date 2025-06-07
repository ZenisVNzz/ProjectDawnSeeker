using UnityEngine;

[CreateAssetMenu(fileName = "ArmoredSkeleton_Skill2", menuName = "Skills/ArmoredSkeleton/ArmoredSkeleton_Skill2")]
public class ArmoredSkeleton_Skill2 : SkillBase
{
	public StatusEffect atkDown;

	public override void DoAction(CharacterInBattle user, CharacterInBattle target)
	{
		target.TakeDamage(user.ATK * 1.2f, 4, user, target);
		target.isPenetrating = true;
		base.DoAction(user, target);
	}

    public override void ApplyEffectOnFinishedAttack(CharacterInBattle user, CharacterInBattle target)
    {
        target.ApplyStatusEffect(atkDown, 1);
    }
}