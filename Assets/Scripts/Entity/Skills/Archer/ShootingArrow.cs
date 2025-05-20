using UnityEngine;

[CreateAssetMenu(fileName = "ShootingArrow", menuName = "Skills/Archer/ShootingArrow")]
public class ShootingArrow : SkillBase
{
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK, 1, user, target);
        base.DoAction(user, target);
    }
}
