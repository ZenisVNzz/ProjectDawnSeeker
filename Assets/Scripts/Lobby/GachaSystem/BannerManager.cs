using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using TMPro;

public class BannerManager : MonoBehaviour
{
    public List<BannerUI> banners;
    private GameObject currentBannerInfo;
    private Banner currentBanner;
    private Button currentBannerButton;

    public Transform pointer;
    public GameObject onChangeAni;

    public TextMeshProUGUI summonX1Consume;
    public TextMeshProUGUI summonX10Consume;

    private void Start()
    {
        BannerUI defaultBanner = banners[0];
        currentBanner = defaultBanner.bannerData;
        currentBannerInfo = defaultBanner.bannerInfo;
        currentBannerButton = defaultBanner.bannerButton;
        foreach (var bannerUI in banners)
        {
            bannerUI.bannerButton.onClick.RemoveAllListeners();
            bannerUI.bannerButton.onClick.AddListener(() =>
            {
                if (currentBannerInfo != bannerUI.bannerInfo)
                {
                    bannerUI.bannerButton.GetComponent<Animator>().Play("Selected");
                    currentBannerButton.GetComponent<Animator>().Play("Unselect");
                    pointer.DOMoveY(bannerUI.bannerButton.transform.position.y, 0.6f);
                    StartCoroutine(WaitForChangeAni(bannerUI));           
                }             
            });
        }
    }

    public void ChangeBanner(BannerUI bannerUI)
    {
        currentBannerInfo.SetActive(false);
        bannerUI.bannerInfo.SetActive(true);
        currentBannerInfo = bannerUI.bannerInfo;
        currentBannerButton = bannerUI.bannerButton;
        SummonUnit summonUnit = FindAnyObjectByType<SummonUnit>();
        summonUnit.currentBanner = bannerUI.bannerData;
        currentBanner = bannerUI.bannerData;
        Debug.Log($"Current Banner: {currentBanner.name}");
        if (bannerUI.bannerData.name == "StandardBanner")
        {
            summonX1Consume.text = "100";
            summonX10Consume.text = "1000";
        }
        else
        {
            summonX1Consume.text = "200";
            summonX10Consume.text = "2000";
        }   
        
    }

    IEnumerator WaitForChangeAni(BannerUI bannerUI)
    {
        onChangeAni.GetComponent<Animator>().Play("OnChange");
        yield return new WaitForSeconds(0.43f);
        ChangeBanner(bannerUI);
    }    
}

[System.Serializable]
public class BannerUI
{
    public Button bannerButton;
    public GameObject bannerInfo;
    public Banner bannerData;
}
