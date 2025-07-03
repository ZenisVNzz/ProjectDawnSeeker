using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "IceShock", menuName = "Skills/Wizard/IceShock")]
public class IceShock : SkillBase
{
    public StatusEffect paralysis;
    public StatusEffect heatShock;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        if (target.activeStatusEffect.Any(e => e.ID == 200012))
        {
            target.TakeDamage(user.ATK * 1.55f, 1, user, target);
            target.ApplyStatusEffect(paralysis, 0);
        }
        else
        {
            target.TakeDamage(user.ATK * 1.35f, 1, user, target);
        }
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnFinishedAttack(CharacterInBattle user, CharacterInBattle target)
    {
        if (target.activeStatusEffect.Any(e => e.ID == 200012))
        {
            target.ApplyStatusEffect(heatShock, 3);
        }
    }
}
