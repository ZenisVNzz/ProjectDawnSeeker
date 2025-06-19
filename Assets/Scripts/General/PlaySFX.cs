using UnityEngine;
using UnityEngine.UI;

public class PlaySFX : MonoBehaviour
{
    public string sfxName;
    public bool playOnAwake;
    public bool toggle;
    public bool customVolume;
    public float volume;

    private void Start()
    {
        if (!toggle)
        {
            if (playOnAwake)
            {
                if (customVolume)
                {
                    SFXManager.instance.PlayWithCustomVol(sfxName, volume);
                }
                else
                {
                    SFXManager.instance.Play(sfxName);
                }
            }   
            else
            {
                Button button = GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    if (customVolume)
                    {
                        SFXManager.instance.PlayWithCustomVol(sfxName, volume);
                    }
                    else
                    {
                        SFXManager.instance.Play(sfxName);
                    }
                });
            }               
        }    
        else
        {
            Toggle toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener((bool isOn) =>
            {
                if (isOn)
                {
                    if (customVolume)
                    {
                        SFXManager.instance.PlayWithCustomVol("Switch_Off", volume);
                    }    
                    else
                    {
                        SFXManager.instance.Play("Switch_Off");
                    }                      
                }    
                else
                {
                    if (customVolume)
                    {
                        SFXManager.instance.PlayWithCustomVol("Switch_On", volume);
                    }
                    else
                    {
                        SFXManager.instance.Play("Switch_On");
                    }                    
                }    
            });
        }    
    }
}
