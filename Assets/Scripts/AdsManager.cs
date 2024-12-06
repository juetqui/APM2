using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsShowListener
{
    [SerializeField] string gameID = "5743370";
    [SerializeField] string adID = "Rewarded_Android";
    [SerializeField] private PlayerPrefsController _prefs;

    private void Start()
    {
        Advertisement.Initialize(gameID, true, this);
    }

    public void OnInitializationComplete()
    {
        Advertisement.Load(adID);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {

    }

    public void ShowAD()
    {
        if (!Advertisement.isInitialized) return;

        Advertisement.Show(adID, this);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {

    }

    public void OnUnityAdsShowStart(string placementId)
    {

    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if(showCompletionState == UnityAdsShowCompletionState.COMPLETED)
            _prefs.SaveForAdd(30);
    }
}
