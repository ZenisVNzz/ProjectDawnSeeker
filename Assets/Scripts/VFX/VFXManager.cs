using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public List<Effect> effect;
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
            //Destroy(effectInstance, 2f);
        }
        else
        {
            Debug.LogWarning($"Effect {ID} not found!");
        }
    }
}

[System.Serializable]
public class Effect
{
    public int ID;
    public GameObject effectPrefab;
}

