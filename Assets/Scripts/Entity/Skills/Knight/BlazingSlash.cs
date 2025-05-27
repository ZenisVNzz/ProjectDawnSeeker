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
            target.TakeDamage(user.ATK * 2.25f, 1, user, target);
            target.ApplyStatusEffect(burn, 2);
        }
        else
        {
            target.TakeDamage(user.ATK * 1.8f, 1, user, target);
            target.ApplyStatusEffect(burn, 2);
        }

        base.DoAction(user, target);
    }
}
