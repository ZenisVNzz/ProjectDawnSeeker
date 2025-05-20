using UnityEngine;

[CreateAssetMenu(fileName = "Meditation", menuName = "Skills/Priest/Meditation")]
public class Meditation : SkillBase
{
    public StatusEffect meditation;
    public override void DoAction(CharacterInBattle user, CharacterInBattle target)
    {
        user.ApplyStatusEffect(meditation, 1);
        base.DoAction(user, target);
    }
}
