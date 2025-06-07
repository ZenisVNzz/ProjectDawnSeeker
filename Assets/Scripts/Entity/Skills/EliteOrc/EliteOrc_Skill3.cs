using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "EliteOrc_Skill3", menuName = "Skills/EliteOrc/EliteOrc_Skill3")]
public class EliteOrc_Skill3 : SkillBase
{
    public StatusEffect atkDown;
    public StatusEffect bleed;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 1.4f, 1, user, target);
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnFinishedAttack(CharacterInBattle user, CharacterInBattle target)
    {
        if(target.activeStatusEffect.Any(e => e.ID == 200011))
        {
            target.ApplyStatusEffect(atkDown, 2);
            target.ApplyStatusEffect(bleed, 2);
        }
    } 
}