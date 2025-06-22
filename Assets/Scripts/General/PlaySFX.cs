using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaySFX : MonoBehaviour, IPointerClickHandler
{
    public string sfxName;
    public bool playOnAwake;
    public bool forEvent;
    public bool pointerClickHandler;
    public bool toggle;
    public bool customVolume;
    public float volume;

    private void Start()
    {
        if (!pointerClickHandler && !forEvent)
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (pointerClickHandler)
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
    }

    public void PlaySound(string soudName)
    {
        if (customVolume)
        {
            SFXManager.instance.PlayWithCustomVol(soudName, volume);
        }
        else
        {
            SFXManager.instance.Play(soudName);
        }
    }
}
