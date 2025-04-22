using UnityEngine;

public abstract class SkillBase : ScriptableObject
{
    public int ID;
    public string skillName;
    public string description;
    public Sprite icon;
    public float mpCost;

    public abstract void DoAction(CharacterInBattle user, CharacterInBattle target);
}
