using UnityEngine;

[CreateAssetMenu(fileName = "OrcRider_Skill2", menuName = "Skills/OrcRider/OrcRider_Skill2")]
public class OrcRider_Skill2 : SkillBase
{
    public StatusEffect paralysis;
    public StatusEffect defDown;
    public StatusEffect bloodThirsty;

    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.TakeDamage(user.ATK * 1.4f, 1, user, target);
        int ran = Random.Range(0, 100);
        if (ran < 60)
        {
            target.ApplyStatusEffect(paralysis, 1);
            target.ApplyStatusEffect(defDown, 3);
        }
        user.ApplyStatusEffect(bloodThirsty, 99);
        base.DoAction(user, target);
    }
}
