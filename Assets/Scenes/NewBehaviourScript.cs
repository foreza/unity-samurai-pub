using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class NewBehaviourScript : MonoBehaviour
{
    Button m_Button;
    public string gameId = "2624011";
    public string placementId = "rewardedVideo";

    void Start()
    {
        m_Button = GetComponent<Button>();
        if (m_Button) {
            Debug.Log("m_Button - AddListener ShowAd");
            m_Button.onClick.AddListener(ShowAd);
            m_Button.enabled = true;
        }

        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, true);
            Debug.Log("Advertisement is supported - game id initialized: " + Advertisement.isInitialized);
        }

        Debug.Log("Successful start");


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
}
