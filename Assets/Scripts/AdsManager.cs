using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsShowListener
{
    [SerializeField] string gameID = "5743370";
    [SerializeField] string adBannerID = "Banner_Android";
    [SerializeField] string adInterstitialID = "Interstitial_Android";
    [SerializeField] string adRewardedID = "Rewarded_Android";
    [SerializeField] private PlayerPrefsController _prefs;

    void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameID, true, this);
        }
    }

    public void OnInitializationComplete()
    {
        Advertisement.Load(adInterstitialID);
        Advertisement.Load(adRewardedID);

        if(SceneManager.GetActiveScene().name == "MainMenu_Start") 
            LoadBanner();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void LoadBanner()
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Load(adBannerID, new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerFailedToLoad
        });
    }

    void OnBannerLoaded()
    {
        Advertisement.Banner.Show(adBannerID);
        Debug.Log("Banner cargado y mostrado.");
    }

    void OnBannerFailedToLoad(string message)
    {
        Debug.LogError("Error al cargar el banner: " + message);
    }

    public void ShowInterstitialAD()
    {
        if (!Advertisement.isInitialized) return;

        Advertisement.Show(adInterstitialID, this);
    }

    public void ShowRewardedAD()
    {
        if (!Advertisement.isInitialized) return;

        Advertisement.Show(adRewardedID, this);
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            if (placementId == "Rewarded_Android")
                _prefs.SaveForAdd(30);
            else if (placementId == "Interstitial_Android")
                Debug.Log("a");
                _prefs.SaveForAdd(30);
        }
        else return;
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
}
