using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class ActionOrder : MonoBehaviour
{
    public Transform playerContainer;
    public Transform enemyContainer;
    public GameObject charPrefab;
    public Dictionary<int, GameObject> currentAction = new Dictionary<int, GameObject>();

    public void AddAction(CharacterInBattle character, SkillBase skill)
    {
        if (currentAction.ContainsKey(character.characterData.characterID))
        {
            Destroy(currentAction[character.characterData.characterID]);
            currentAction.Remove(character.characterData.characterID);
        }

        GameObject newAction;
        if (character.characterType == characterType.Player)
        {
            newAction = Instantiate(charPrefab, playerContainer);
        }
        else
        {
            newAction = Instantiate(charPrefab, enemyContainer);
        }
        Image charIMG = newAction.transform.Find("Outline").transform.Find("View").transform.Find("CharIMG").GetComponent<Image>();
        Image SkillIMG = newAction.transform.Find("Skill").transform.Find("SkillSlot").transform.Find("Outline").transform.Find("View").Find("SkIMG").GetComponent<Image>();

        charIMG.sprite = character.characterSprite;
        SkillIMG.sprite = skill.icon;

        currentAction.Add(character.characterData.characterID, newAction);
    }

    public void RemoveAction(CharacterInBattle character, SkillBase skill)
    {
        if (currentAction.ContainsKey(character.characterData.characterID))
        {
            ActionUI_FloatIn actionUI = currentAction[character.characterData.characterID].GetComponent<ActionUI_FloatIn>();
            StartCoroutine(actionUI.FloatOut(currentAction[character.characterData.characterID]));
            currentAction.Remove(character.characterData.characterID);
        }
    }    
}
