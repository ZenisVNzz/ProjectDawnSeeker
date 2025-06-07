using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "BlazingSlash", menuName = "Skills/Knight/BlazingSlash")]
public class BlazingSlash : SkillBase
{
    public StatusEffect burn;
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        if (target.activeStatusEffect.Any(effect => effect.ID == 200012))
        {
            target.TakeDamage(user.ATK * 2.1f, 1, user, target);
        }
        else
        {
            target.TakeDamage(user.ATK * 1.7f, 1, user, target);
        }

        base.DoAction(user, target);
    }

    public override void ApplyEffectOnFinishedAttack(CharacterInBattle user, CharacterInBattle target)
    {
        target.ApplyStatusEffect(burn, 2);
    }
}
