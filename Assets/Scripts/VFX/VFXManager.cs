using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VFXManager : MonoBehaviour
{
    public List<Effect> effect;
    public Dictionary<int, GameObject> activeEffectVFX = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> effectDictionary;

    private void Awake()
    {
        effectDictionary = new Dictionary<int, GameObject>();
        foreach (var entry in effect)
        {
            effectDictionary[entry.ID] = entry.effectPrefab;
        }
    }

    public void PlayEffect(int ID, Vector3 position)
    {
        if (effectDictionary.TryGetValue(ID, out GameObject prefab))
        {
            GameObject effectInstance = Instantiate(prefab, position, Quaternion.identity);
            if (effect.Any(e => e.ID == ID && !e.duringEffect))
            {
                Destroy(effectInstance, 2f);
            }
            else
            {
                if (!activeEffectVFX.ContainsKey(ID))
                {
                    activeEffectVFX.Add(ID, effectInstance);
                }
            }
        }
        else
        {
            return;
        }
    }

    public void StopEffect(int ID)
    {
        if (activeEffectVFX.TryGetValue(ID, out GameObject effectInstance))
        {
            Destroy(effectInstance);
            activeEffectVFX.Remove(ID);
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

