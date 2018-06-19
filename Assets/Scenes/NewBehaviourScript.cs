using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;


[RequireComponent(typeof(Button))]
[SuppressMessage("ReSharper", "AccessToStaticMemberViaDerivedType")]

public class NewBehaviourScript : MonoBehaviour
{
    Button m_Button;
    public string gameId = "2624011";
    public string placementId = "rewardedVideo";

    // Mopub
    private readonly string[] _bannerAdUnits =
    { "0ac59b0996d947309c33f59d6676399f" };

    private readonly string[] _interstitialAdUnits =
        { "4f117153f5c24fa6a3a92b818a5eb630" };

    private readonly string[] _rewardedVideoAdUnits =
        { "8f000bd5e00246de9c789eed39ff6096" };

    private readonly string[] _rewardedRichMediaAdUnits = { };

    void Start()
    {

        m_Button = GetComponent<Button>();
        if (m_Button)
        {
            Debug.Log("m_Button - AddListener ShowAd");
            m_Button.onClick.AddListener(ShowAd);
        }

        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, true);
            Debug.Log("Advertisement is supported - game id initialized: " + Advertisement.isInitialized);
        }

        Debug.Log("Successful start");

        var anyAdUnitId = _bannerAdUnits[0];

        MoPub.InitializeSdk(anyAdUnitId);

        MoPub.LoadBannerPluginsForAdUnits(_bannerAdUnits);
        MoPub.LoadInterstitialPluginsForAdUnits(_interstitialAdUnits);
        MoPub.LoadRewardedVideoPluginsForAdUnits(_rewardedVideoAdUnits);
        MoPub.LoadRewardedVideoPluginsForAdUnits(_rewardedRichMediaAdUnits);

        CreateInterstitialsSection();





    }

    void Update()
    {
        if (m_Button) {

            m_Button.interactable = Advertisement.IsReady(placementId);
            Debug.Log("Advertisement is ready?" + Advertisement.IsReady(placementId));

        }

        Debug.Log("m_Button is interactable: " + m_Button.interactable);
    }

    void ShowAd()
    {
        var options = new ShowOptions();
        options.resultCallback = HandleShowResult;

        Advertisement.Show(placementId, options);
        Debug.Log("ShowAd, Advertisement" + placementId);

    }

    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Debug.Log("Video completed - Offer a reward to the player");

        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("Video was skipped - Do NOT reward the player");

        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
        }
    }

    // MoPub Create interstitial
    private void CreateInterstitialsSection()
    {
        MoPub.RequestInterstitialAd(_interstitialAdUnits[0]);
        MoPub.ShowInterstitialAd(_interstitialAdUnits[0]);
          
    }

}
