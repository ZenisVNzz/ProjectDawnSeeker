using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
                Destroy(effectInstance, 1.2f);
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

    public void StopEffect(int charID, int effectID)
    {
        if (activeEffectVFX.ContainsKey(charID))
        {
            if (activeEffectVFX[charID].ContainsKey(effectID))
            {
                Destroy(activeEffectVFX[charID][effectID]);
                activeEffectVFX[charID].Remove(effectID);
            }
        }        
        else
        {
            return;
        }    
    }
}

[System.Serializable]
public class Effect
{
    public int ID;
    public bool duringEffect;
    public GameObject effectPrefab;
}

