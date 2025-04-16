using UnityEngine;

public abstract class SkillBase
{
    public int skillID;
    public string skillName;
    public string skillDescription;
    public Sprite icons;
    public float mpCost;

    public abstract void Action(CharacterInBattle user, CharacterInBattle target);
}
