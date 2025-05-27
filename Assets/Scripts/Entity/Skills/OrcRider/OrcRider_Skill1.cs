using UnityEngine;

[CreateAssetMenu(fileName = "OrcRider_Skill1", menuName = "Skills/OrcRider/OrcRider_Skill1")]
public class OrcRider_Skill1 : SkillBase
{
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK, 1, user, target);
        base.DoAction(user, target);
    }
}
