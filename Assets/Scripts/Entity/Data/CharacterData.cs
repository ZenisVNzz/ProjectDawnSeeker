using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum characterType { Player, Enemy }

[CreateAssetMenu(fileName = "Character", menuName = "Characters/NewCharacter")]
public class CharacterData : ScriptableObject
{
    public int characterID;
    public string characterName;
    public characterType characterType;
    public Sprite characterSprite;
    public RuntimeAnimatorController characterAnimation;
    public bool isBoss = false;

    public List<SkillBase> skillList;

    [Header("Stats")]
    public float level;
    public float ATK;
    public float HP;
    public float DEF;
    public float MP;
    public float CR;
    public float CD;
    public float DC;
    public float PC;

    [Header("Scale")]
    public float maxLevel;
    public float currentXP;
    public float neededXP = 100f;
    public float ATKPerLevel;
    public float HPPerLevel;
    public float DEFPerLevel;
    public float MPPerLevel;
    public float CRPerLevel;
    public float CDPerLevel;

    public void AddXP(int xp)
    {
        currentXP += xp;
        while (currentXP >= neededXP)
        {
            if (!(level + 1 > maxLevel))
            {
                level++;
                currentXP -= neededXP;
                currentXP = Mathf.Round(currentXP);
                neededXP *= 1.04f;
                neededXP = Mathf.Round(neededXP);
                ATK += ATKPerLevel;
                HP += HPPerLevel;
                DEF += DEFPerLevel;
                MP += MPPerLevel;
                CR += CRPerLevel;
                CD += CDPerLevel;
            }    
            else
            {
                currentXP = 0;
                break;
            }    
        }  
    }    
}
