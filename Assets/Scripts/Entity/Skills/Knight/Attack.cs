using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Skills/Knight/Attack", order = 1)]
public class Attack : SkillBase
{
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK, user, target);
        user.savedDmg = user.ATK - target.DEF;
        base.DoAction(user, target);
    }
}
