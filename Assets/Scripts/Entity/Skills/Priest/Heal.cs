using UnityEngine;

[CreateAssetMenu(fileName = "Heals", menuName = "Skills/Priest/Heals")]
public class Heals : SkillBase
{
    public StatusEffect heal;
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        target.ApplyStatusEffect(heal, 1);
    }
}
