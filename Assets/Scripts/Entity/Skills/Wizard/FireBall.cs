using UnityEngine;

[CreateAssetMenu(fileName = "FireBall", menuName = "Skills/Wizard/FireBall")]
public class FireBall : SkillBase
{
    public StatusEffect burn;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 1.3f, 1, user, target);
        target.ApplyStatusEffect(burn, 2);
        base.DoAction(user, target);
    }
}
