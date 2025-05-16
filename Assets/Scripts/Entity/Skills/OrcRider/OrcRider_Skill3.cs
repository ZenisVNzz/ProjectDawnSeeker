using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "OrcRider_Skill3", menuName = "Skills/OrcRider/OrcRider_Skill3")]
public class OrcRider_Skill3 : SkillBase
{
    public StatusEffect fear;
    public StatusEffect atkUP;
    public StatusEffect bloodThirsty;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        BattleManager battleManager = FindAnyObjectByType<BattleManager>();
        foreach (var player in battleManager.TeamPlayer)
        {
            if (player.activeStatusEffect.Any(effect => effect.ID == 200011))
            {
                player.ApplyStatusEffect(fear, 2);
            }
        }
        user.ApplyStatusEffect(atkUP, 2);
        user.ApplyStatusEffect(bloodThirsty, 99);
        base.DoAction(user, target);
    }
}
