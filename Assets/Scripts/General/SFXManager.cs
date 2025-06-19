using UnityEngine;
using System.Collections.Generic;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    public AudioSource audioSource;
    public List<SFX> sfxList;

    private Dictionary<string, AudioClip> sfxDict;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        sfxDict = new Dictionary<string, AudioClip>();
        foreach (var entry in sfxList)
        {
            if (!sfxDict.ContainsKey(entry.sfxName))
            {
                sfxDict.Add(entry.sfxName, entry.clip);
            }
        }
    }

    public void Play(string name)
    {
        if (sfxDict.ContainsKey(name))
        {
            audioSource.PlayOneShot(sfxDict[name]);
        }
        else
        {
            Debug.LogWarning($"SFX with name {name} does not exits");
        }
    }

    public void PlayWithCustomVol(string name, float volume)
    {
        if (sfxDict.ContainsKey(name))
        {
            audioSource.PlayOneShot(sfxDict[name], volume);
        }
        else
        {
            Debug.LogWarning($"SFX with name {name} does not exits");
        }
    }
}

[System.Serializable]
public class SFX
{
    public string sfxName;
    public AudioClip clip;
}

