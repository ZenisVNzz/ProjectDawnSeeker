using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Skills/Knight/Attack", order = 1)]
public class Attack : SkillBase
{
    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK * 1.1f, 1, user, target);
        base.DoAction(user, target);
    }
}
