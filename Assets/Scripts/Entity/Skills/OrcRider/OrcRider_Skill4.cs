using UnityEngine;

[CreateAssetMenu(fileName = "OrcRider_Skill4", menuName = "Skills/OrcRider/OrcRider_Skill4")]
public class OrcRider_Skill4 : SkillBase
{
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        BattleManager battleManager = FindAnyObjectByType<BattleManager>();
        foreach (var player in battleManager.TeamPlayer)
        {
            if (player.isAlive)
            {
                player.TakeDamage(user.ATK * 4.5f, 3, user, target);
            }    
        }
        base.DoAction(user, target);
    }
}
