using NUnit.Framework;
using UnityEngine;

public enum characterType { Player, Enemy }

[CreateAssetMenu(fileName = "Character", menuName = "Characters/NewCharacter")]
public class CharacterData : ScriptableObject
{
    public int characterID;
    public string characterName;
    public characterType characterType;
    public Sprite characterSprite;
    public Animator characterAnimation;
    public float ATK;
    public float HP;
    public float DEF;
    public float MP;
    public float CR;
    public float CD;
    public float DC;
    public float PC;    
}
