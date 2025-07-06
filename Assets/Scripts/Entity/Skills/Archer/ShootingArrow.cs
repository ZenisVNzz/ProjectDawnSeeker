using UnityEngine;

[CreateAssetMenu(fileName = "ShootingArrow", menuName = "Skills/Archer/ShootingArrow")]
public class ShootingArrow : SkillBase
{
    public override void DoAction(CharacterRuntime user, CharacterRuntime target)
    {
        target.TakeDamage(user.ATK, 1, user, target);
        base.DoAction(user, target);
    }
}
