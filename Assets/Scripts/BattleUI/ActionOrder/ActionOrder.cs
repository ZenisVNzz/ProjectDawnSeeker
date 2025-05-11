using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class ActionOrder : MonoBehaviour
{
    public Transform playerContainer;
    public Transform enemyContainer;
    public GameObject charPrefab;
    public Dictionary<int, List<GameObject>> currentAction = new Dictionary<int, List<GameObject>>();

    public void AddAction(CharacterInBattle character, CharacterInBattle target, SkillBase skill, bool isTargetAlly)
    {
        int charID = character.characterData.characterID;

        if (character.characterType != characterType.Enemy && currentAction.ContainsKey(charID))
        {
            foreach (GameObject obj in currentAction[charID])
            {
                if (obj != null)
                    Destroy(obj);
            }
            currentAction[charID].Clear();
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

        DataStorage dataStorage = newAction.GetComponent<DataStorage>();
        dataStorage.attacker = character;
        dataStorage.target = target;
        dataStorage.isTargetAlly = isTargetAlly;
        dataStorage.isPassiveSkill = skill.passiveSkill;

        Image charIMG = newAction.transform.Find("Outline/View/CharIMG").GetComponent<Image>();
        Image SkillIMG = newAction.transform.Find("Skill/SkillSlot/Outline/View/SkIMG").GetComponent<Image>();

        if (character.characterType == characterType.Enemy)
        {
            GameObject skillObj = SkillIMG.gameObject;
            skillObj.transform.localEulerAngles = new Vector3(180f, 0f, 225f);
        }

        charIMG.sprite = character.characterSprite;
        SkillIMG.sprite = skill.icon;

        if (!currentAction.ContainsKey(charID))
        {
            currentAction[charID] = new List<GameObject>();
        }

        currentAction[charID].Add(newAction);
    }


    public void RemoveAction(CharacterInBattle character, SkillBase skill)
    {
        int charID = character.characterData.characterID;

        if (currentAction.ContainsKey(charID))
        {
            List<GameObject> actionList = currentAction[charID];
            GameObject toRemove = null;

            foreach (var obj in actionList)
            {
                Image skillIMG = obj.transform.Find("Skill/SkillSlot/Outline/View/SkIMG").GetComponent<Image>();
                if (skillIMG != null && skillIMG.sprite == skill.icon)
                {
                    toRemove = obj;
                    break;
                }
            }

            if (toRemove != null)
            {
                ActionUI_FloatIn actionUI = toRemove.GetComponent<ActionUI_FloatIn>();
                StartCoroutine(actionUI.FloatOut(toRemove));
                actionList.Remove(toRemove);
            }

            if (actionList.Count == 0)
            {
                currentAction.Remove(charID);
            }
        }
    }
}
