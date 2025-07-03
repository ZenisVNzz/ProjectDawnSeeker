using UnityEngine;

namespace BGMLooperSystem
{
    public static class BGMLooper
    {
        public static void Loop(AudioSource audioSource, AudioClip loopClip)
        {
            if (audioSource != null && loopClip != null)
            {
                audioSource.loop = true;
                audioSource.clip = loopClip;
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("AudioSource or Loop Clip is not set.");
            }
        }
    }
}
