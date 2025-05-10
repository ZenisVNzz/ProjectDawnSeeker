using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TargetArrow : MonoBehaviour
{
    public static Dictionary<int, LineRenderer> lineRenderer = new Dictionary<int, LineRenderer>();
    public GameObject arrow;
    public static Dictionary<int, Dictionary<int, GameObject>> arrowInstances = new Dictionary<int, Dictionary<int, GameObject>>();
    public int segmentCount = 20;
    public float curveHeight = 3f;

    void Start()
    {
        CharacterInBattle characterInBattle = GetComponent<CharacterInBattle>();
        lineRenderer.Add(characterInBattle.characterData.characterID, transform.GetComponentInChildren<LineRenderer>());
        lineRenderer[characterInBattle.characterData.characterID].positionCount = segmentCount + 1;
    }

    public void MakeArrow(CharacterInBattle attacker, CharacterInBattle target, bool isTargetAlly)
    {
        if (arrowInstances.ContainsKey(attacker.characterData.characterID) && attacker.characterType != characterType.Enemy)
        { 
            foreach (var arrowInstance in arrowInstances[attacker.characterData.characterID].Values)
            {
                if (arrowInstance != null)
                    GameObject.Destroy(arrowInstance);
            }
            arrowInstances[attacker.characterData.characterID].Clear();
            if (arrowInstances[attacker.characterData.characterID].Count == 0)
            {
                arrowInstances.Remove(attacker.characterData.characterID);
            }
        }
        if (attacker != null && target != null)
        {
            lineRenderer[attacker.characterData.characterID].enabled = true;

            Vector3 startPosition;
            Vector3 endPosition;

            if (isTargetAlly)
            {
                startPosition = attacker.transform.position + Vector3.up * 0.85f + Vector3.right * 0.1f;
                endPosition = target.transform.position + Vector3.up * 1.15f + Vector3.left * 0.1f;
            }
            else if (attacker.characterType == characterType.Enemy)
            {
                startPosition = attacker.transform.position + Vector3.up * 0.85f + Vector3.left * 0.1f;
                endPosition = target.transform.position + Vector3.up * 1.15f + Vector3.right * 0.1f;
            }
            else
            {
                startPosition = attacker.transform.position + Vector3.up * 0.85f + Vector3.right * 0.6f;
                endPosition = target.transform.position + Vector3.up * 1.15f + Vector3.left * 0.6f;
            }
            

            Vector3 mid = (startPosition + endPosition) / 2 + Vector3.up * curveHeight;

            for (int i = 0; i <= segmentCount; i++)
            {
                float t = i / (float)segmentCount;
                Vector3 point = CalculateQuadraticBezierPoint(t, startPosition, mid, endPosition);
                lineRenderer[attacker.characterData.characterID].SetPosition(i, point);
            }

            if (!arrowInstances.ContainsKey(attacker.characterData.characterID))
            {
                arrowInstances.Add(attacker.characterData.characterID, new Dictionary<int, GameObject>());
                bool targetExists = arrowInstances.Values.Any(innerDict => innerDict.ContainsKey(target.characterData.characterID));
                if (arrow != null && !targetExists)
                {
                    GameObject arrowInstance = Instantiate(arrow);
                    Vector3 pointA = lineRenderer[attacker.characterData.characterID].GetPosition(segmentCount - 1);
                    Vector3 pointB = lineRenderer[attacker.characterData.characterID].GetPosition(segmentCount);
                    Vector3 dir = (pointB - pointA).normalized;
                    arrowInstance.transform.position = pointB;
                    arrowInstance.transform.right = dir;
                    arrowInstances[attacker.characterData.characterID].Add(target.characterData.characterID, arrowInstance);
                }
            }

        }
        else
        {
            lineRenderer[attacker.characterData.characterID].enabled = false;
            return;
        }    
    }

    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        return u * u * p0 + 2 * u * t * p1 + t * t * p2;
    }

    public void RemoveArrow(CharacterInBattle attacker)
    {
        if (arrowInstances.ContainsKey(attacker.characterData.characterID))
        {
            foreach (var arrowInstance in arrowInstances[attacker.characterData.characterID].Values)
            {
                if (arrowInstance != null)
                    GameObject.Destroy(arrowInstance);
            }
            arrowInstances[attacker.characterData.characterID].Clear();
            if (arrowInstances[attacker.characterData.characterID].Count == 0)
            {
                arrowInstances.Remove(attacker.characterData.characterID);
            }
        }
        lineRenderer[attacker.characterData.characterID].enabled = false;
    }    
}
