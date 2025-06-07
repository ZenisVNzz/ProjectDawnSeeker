using UnityEngine;

[CreateAssetMenu(fileName = "ArmoredOrc_Skill3", menuName = "Skills/ArmoredOrc/ArmoredOrc_Skill3")]
public class ArmoredOrc_Skill3 : SkillBase
{
    public StatusEffect defDown;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        BattleManager battleManager = FindAnyObjectByType<BattleManager>();
        foreach (var player in battleManager.TeamPlayer)
        {
            if (player.isAlive)
            {
                player.TakeDamage(user.ATK * 1.25f, 1, user, target);
                player.ApplyStatusEffect(defDown, 2);
            }    
        }
        base.DoAction(user, target);
    }
}