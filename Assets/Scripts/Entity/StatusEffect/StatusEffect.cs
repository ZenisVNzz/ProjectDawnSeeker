using UnityEngine;

public enum StatusType { Buff, Debuff }

public abstract class StatusEffect : ScriptableObject
{
    public int ID;
    public string statusName;
    public StatusType type;
    public int duration;
    public Sprite Icon;

    public abstract void OnApply(CharacterInBattle target);
    public abstract void OnTurn(CharacterInBattle target);
    public abstract void OnRemove(CharacterInBattle target);
}
