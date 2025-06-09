using UnityEngine;

[CreateAssetMenu(fileName = "KnightTemplar_Skill1", menuName = "Skills/KnightTemplar/KnightTemplar_Skill1")]
public class KnightTemplar_Skill1: SkillBase
{
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 1.0f, 1, user, target);
        base.DoAction(user, target);
    }
}
