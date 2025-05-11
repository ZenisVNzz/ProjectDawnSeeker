using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class VFXManager : MonoBehaviour
{
    public List<Effect> effect;
    public Dictionary<int, Dictionary<int, GameObject>> activeEffectVFX = new Dictionary<int, Dictionary<int, GameObject>> ();
    private Dictionary<int, GameObject> effectDictionary;

    private void Awake()
    {
        effectDictionary = new Dictionary<int, GameObject>();
        foreach (var entry in effect)
        {
            effectDictionary[entry.ID] = entry.effectPrefab;
        }
    }

    public void PlayEffect(int ID, Vector3 position, int charID)
    {
        if (effectDictionary.TryGetValue(ID, out GameObject prefab))
        {
            GameObject effectInstance = Instantiate(prefab, position, Quaternion.identity);
            if (effect.Any(e => e.ID == ID && !e.duringEffect))
            {
                Destroy(effectInstance, 3f);
            }
            else
            {
                if (!activeEffectVFX.ContainsKey(charID))
                {
                    activeEffectVFX.Add(charID, new Dictionary<int, GameObject>());
                    activeEffectVFX[charID].Add(ID, effectInstance);
                }
                else
                {
                    activeEffectVFX[charID].Add(ID, effectInstance);
                }
            }   
        }
        else
        {
            return;
        }
    }

    public IEnumerator StopEffect(int charID, int effectID)
    {
        yield return new WaitForSeconds(3f);
        if (activeEffectVFX.ContainsKey(charID))
        {
            if (activeEffectVFX[charID].ContainsKey(effectID))
            {
                Destroy(activeEffectVFX[charID][effectID]);
                activeEffectVFX[charID].Remove(effectID);
            }  
        }
    }

    public void StopAllEffect(int charID)
    {
        if (activeEffectVFX.ContainsKey(charID))
        {
            foreach (var effect in activeEffectVFX[charID])
            {
                Destroy(effect.Value);
            }
            activeEffectVFX.Remove(charID);
        }
    }    
}

[System.Serializable]
public class Effect
{
    public int ID;
    public bool duringEffect;
    public bool isPlayOnHit;
    public GameObject effectPrefab;
}

