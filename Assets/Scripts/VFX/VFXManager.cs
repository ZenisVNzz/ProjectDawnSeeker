using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;
using System;
using System.Threading.Tasks;

public class VFXManager : MonoBehaviour
{
    public List<Effect> effect;
    private Dictionary<int, Dictionary<int, Queue<Vector3>>> queuedEffects = new Dictionary<int, Dictionary<int, Queue<Vector3>>>();
    private Dictionary<int, Dictionary<int, bool>> isPlaying = new Dictionary<int, Dictionary<int, bool>>();
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

    public async Task PlayEffect(int ID, Vector3 position, CharacterRuntime character)
    {
        GameObject effectInstance;

        if (effect.Any(e => e.ID == ID && !e.isMove))
        {
            int charID = character.characterData.characterID;
            if (effectDictionary.TryGetValue(ID, out GameObject prefab))
            {
                effectInstance = Instantiate(prefab, position, Quaternion.identity);
                if (effect.Any(e => e.ID == ID && !e.duringEffect))
                {
                    Destroy(effectInstance, 5f);
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

            Animator animator = effectInstance.GetComponent<Animator>();
            while (animator != null && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                await Task.Yield();
            }
        }
        else
        {
            if (effectDictionary.TryGetValue(ID, out GameObject prefab))
            {
                effectInstance = Instantiate(prefab, character.transform.position + Vector3.right * 1.5f, Quaternion.identity);
                Vector3 dir = position - effectInstance.transform.position;
                float angleZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                effectInstance.transform.rotation = Quaternion.Euler(0, 0, angleZ);
                EffectMover effectMover = effectInstance.GetComponent<EffectMover>();
                Animator animator = effectInstance.GetComponent<Animator>();
                effectMover.target = position;
                effectMover.onHit = () =>
                {
                    animator.Play("Hit");
                    Destroy(effectInstance, 2f);
                    character.WaitForRangeSkillHit();
                };
                effectMover.MoveToTarget();
            }
        }
    }

    public IEnumerator StopEffect(int charID, int effectID)
    {
        yield return new WaitForSeconds(1f);
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
    public bool isMove;
    public GameObject effectPrefab;
}

