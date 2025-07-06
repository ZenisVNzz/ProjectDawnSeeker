using UnityEngine;

[CreateAssetMenu(fileName = "KnightTemplar_Skill1", menuName = "Skills/KnightTemplar/KnightTemplar_Skill1")]
public class KnightTemplar_Skill1: SkillBase
{
    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK * 1.1f, 1, user, target);
        base.DoAction(user, target);
    }
}
