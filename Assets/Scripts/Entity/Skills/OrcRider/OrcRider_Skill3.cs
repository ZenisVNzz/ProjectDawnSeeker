using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "OrcRider_Skill3", menuName = "Skills/OrcRider/OrcRider_Skill3")]
public class OrcRider_Skill3 : SkillBase
{
    public StatusEffect fear;
    public StatusEffect atkUP;
    public StatusEffect bloodThirsty;

    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        BattleManager battleManager = FindAnyObjectByType<BattleManager>();
        foreach (var player in battleManager.TeamPlayer)
        {
            if (player.activeStatusEffect.Any(effect => effect.ID == 200011))
            {
                player.ApplyStatusEffect(fear, 2);
            }
        }
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnEnd(CharacterRuntime user, CharacterRuntime target)
    {
        user.ApplyStatusEffect(atkUP, 2);
        user.ApplyStatusEffect(bloodThirsty, 99);
    }
}
