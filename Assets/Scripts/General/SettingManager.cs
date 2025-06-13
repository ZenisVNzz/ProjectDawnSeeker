using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown languageDropdown;
    public Toggle toggleFullScreen;

    private Resolution[] resolutions;
    private static bool isFullScreen = true;
    private int defaultIndex;
    public static int selectedResolutionIndex;
    public static int selectedLanguageIndex;
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

        languageDropdown.ClearOptions();
        List<string> languageList = new List<string> { "English", "Vietnamese" };
        languageDropdown.AddOptions(languageList);
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

    public void ChangeLanguage()
    {
        selectedLanguageIndex = languageDropdown.value;
        if (selectedLanguageIndex == 0)
        {
            Locale selectedLocale = LocalizationSettings.AvailableLocales.GetLocale("en");
            LocalizationSettings.SelectedLocale = selectedLocale;
        }
        else if (selectedLanguageIndex == 1)
        {
            Locale selectedLocale = LocalizationSettings.AvailableLocales.GetLocale("vi");
            LocalizationSettings.SelectedLocale = selectedLocale;
        }
    }

    public void ReturnToTitleScreen()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void OnExitGame()
    {
        Invoke("ExitGame", 0.5f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }    

    public void ApplySettings()
    {
        ChangeRes();
        ChangeFullScreen();
        ChangeLanguage();
    }
}
