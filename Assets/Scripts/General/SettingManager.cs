using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public Toggle toggleFullScreen;

    private Resolution[] resolutions;
    private static bool isFullScreen = true;
    private int defaultIndex;
    public static int selectedResolutionIndex;
    List<Resolution> selectedResolutions = new List<Resolution>();

    private void Start()
    {
        toggleFullScreen.isOn = isFullScreen;
        resolutions = Screen.resolutions;

        List<string> resolutionListString = new List<string>();
        string newRes;
        foreach (Resolution res in resolutions)
        {
            newRes = res.width.ToString() + " x " + res.height.ToString();
            if (!resolutionListString.Contains(newRes))
            {
                resolutionListString.Add(newRes);
                selectedResolutions.Add(res);              
            }
            if (res.width == Screen.currentResolution.width &&
                res.height == Screen.currentResolution.height)
            {
                defaultIndex = resolutionListString.Count - 1;
            }
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutionListString);

        resolutionDropdown.value = defaultIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void ChangeRes()
    {
        selectedResolutionIndex = resolutionDropdown.value;
        Screen.SetResolution(selectedResolutions[selectedResolutionIndex].width,
                           selectedResolutions[selectedResolutionIndex].height,
                           isFullScreen);
    }

    public void ChangeFullScreen()
    {
        isFullScreen = toggleFullScreen.isOn;
        Screen.SetResolution(selectedResolutions[selectedResolutionIndex].width,
                           selectedResolutions[selectedResolutionIndex].height,
                           isFullScreen);
    }

    public void ApplySettings()
    {
        ChangeRes();
        ChangeFullScreen();
    }
}
