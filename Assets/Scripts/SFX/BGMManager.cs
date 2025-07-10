using System.Collections;
using UnityEngine;
using BGMLooperSystem;

public class BGMManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource loopSource;

    void Start()
    { 
        StartCoroutine(PlayBGM());
    }

    IEnumerator PlayBGM()
    {
        yield return new WaitForSeconds(0.5f);
        if (audioSource != null && !audioSource.isPlaying)
        {
            StageData stageData = FindAnyObjectByType<StageData>();
            audioSource.clip = stageData.GetBGMClip();
            loopSource.clip = stageData.GetBGMLoop();
            audioSource.Play();
            loopSource.PlayDelayed(audioSource.clip.length);
        }
    }
}
