using UnityEngine;
using Unity.Services.RemoteConfig;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using TMPro;

public class RemoteConfig : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttributes { }

    [SerializeField] StaminaSystem staminaSystem;
    [SerializeField] PlayerPrefsController prefs;

    [SerializeField] GameObject serverOutPanel;
    [SerializeField] TextMeshProUGUI versionText;
    //[SerializeField] GameManager GM;
    [SerializeField] TextMeshProUGUI title;

    public int startCurrency = default, maxStamina = default, staminaCooldown = default;

    private void Awake()
    {
        StartProcess();
    }

    async void StartProcess()
    {
        if (Utilities.CheckForInternetConnection())
        {
            await InitializeRemoteConfig();
        }
        else
        {

        }

        RemoteConfigService.Instance.FetchCompleted += Fetch;
        RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
    }

    async Task InitializeRemoteConfig()
    {
        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    void Fetch(ConfigResponse response)
    {
        if(serverOutPanel != null)
            serverOutPanel.SetActive(RemoteConfigService.Instance.appConfig.config.Value<bool>("Bool_ServerOut"));

        if (versionText != null)
            versionText.text = RemoteConfigService.Instance.appConfig.config.Value<int>("Int_Version").ToString();

        if (title != null)
            title.text = RemoteConfigService.Instance.appConfig.config.Value<string>("Str_Name");

        startCurrency = RemoteConfigService.Instance.appConfig.config.Value<int>("Int_StartCurrency");
        maxStamina = RemoteConfigService.Instance.appConfig.config.Value<int>("Int_MaxStamina");
        staminaCooldown = RemoteConfigService.Instance.appConfig.config.Value<int>("Int_StaminaCooldown");

        prefs.Init(startCurrency);
        staminaSystem.Init(maxStamina, staminaCooldown);
    }
}

