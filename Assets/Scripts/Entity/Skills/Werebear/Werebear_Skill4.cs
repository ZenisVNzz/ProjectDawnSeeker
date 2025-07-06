using System.Linq;
using Unity.Jobs;
using UnityEngine;

[CreateAssetMenu(fileName = "Werebear_Skill4", menuName = "Skills/Werebear/Werebear_Skill4")]
public class Werebear_Skill4 : SkillBase
{
    public StatusEffect bleed;

    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        if (target.activeStatusEffect.Any(e => e.ID == 200013))
        {
            target.TakeDamage(user.ATK * 1.4f, 1, user, target);
        }
        else
        {
            target.TakeDamage(user.ATK * 1.25f, 1, user, target);
        }   
        base.DoAction(user, target);
    }

    public override void ApplyEffectOnEnd(CharacterRuntime user, CharacterRuntime target)
    {
        BattleManager battleManager = FindAnyObjectByType<BattleManager>();
        foreach (CharacterRuntime character in battleManager.TeamPlayer)
        {
            character.ApplyStatusEffect(bleed, 2);
        }
    }
}
