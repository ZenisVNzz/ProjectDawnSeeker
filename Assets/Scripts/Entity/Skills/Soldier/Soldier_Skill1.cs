using UnityEngine;

[CreateAssetMenu(fileName = "Soldier_Skill1", menuName = "Skills/Soldier/Soldier_Skill1")]
public class Soldier_Skill1 : SkillBase
{
    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK, 1, user, target); 
        base.DoAction(user, target);

    }
}
