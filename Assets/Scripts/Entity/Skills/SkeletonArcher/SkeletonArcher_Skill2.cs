using UnityEngine;

[CreateAssetMenu(fileName = "SkeletonArcher_Skill2", menuName = "Skills/SkeletonArcher/SkeletonArcher_Skill2")]
public class SkeletonArcher_Skill2 : SkillBase
{
    public StatusEffect crUP;
    public StatusEffect atkUP;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        user.ApplyStatusEffect(crUP, 2);
        user.ApplyStatusEffect(atkUP, 2);
        base.DoAction(user, target);
    }
}