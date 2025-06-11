using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ArmoredAxeman_Skill4", menuName = "Skills/ArmoredAxeman_Skill4/ArmoredAxeman_Skill4")]
public class ArmoredAxeman_Skill4 : SkillBase
{
    public StatusEffect deadlyBlood;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 1.5f, 2, user, target);
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnFinishedAttack(CharacterInBattle user, CharacterInBattle target)
    {
        if (target.activeStatusEffect.Any(effect => effect.ID == 200013))
        {
            target.ApplyStatusEffect(deadlyBlood, 3);
        }
    }
}

