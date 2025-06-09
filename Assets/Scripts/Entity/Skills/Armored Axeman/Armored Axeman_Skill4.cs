using UnityEngine;

[CreateAssetMenu(fileName = "ArmoredAxeman_Skill4", menuName = "Skills/ArmoredAxeman_Skill4/ArmoredAxeman_Skill4")]
public class ArmoredAxeman_Skill4 : SkillBase
{
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 1.6f, 1, user, target);
        base.DoAction(user, target);
    }
}

