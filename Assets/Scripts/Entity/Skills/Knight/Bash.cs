using UnityEngine;

[CreateAssetMenu(fileName = "Bash", menuName = "Skills/Knight/Bash")]
public class Bash : SkillBase
{
    public StatusEffect paralysis;
    public StatusEffect defUP;
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        float damage = user.ATK * 0.3f;
        target.TakeDamage(damage, 1, user, target);
        user.ApplyStatusEffect(defUP, 2);
        int ran = Random.Range(0, 100);
        if (ran < 50)
        {
            target.ApplyStatusEffect(paralysis, 1);
        }
        base.DoAction(user, target);
    }
}
