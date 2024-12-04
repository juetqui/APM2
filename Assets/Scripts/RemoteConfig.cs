using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.RemoteConfig;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using Unity.VisualScripting;
using TMPro;

public class RemoteConfig : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttributes { }

    [SerializeField] GameObject serverOutPanel;
    [SerializeField] TextMeshProUGUI versionText;
    //[SerializeField] GameManager GM;
    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] TextMeshProUGUI looseText;

    private void Start()
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

        //if (GM != null)
        //    GM.changeObjective(RemoteConfigService.Instance.appConfig.config.Value<int>("Int_EnemiesObj"));

        if (winText != null)
            winText.text = RemoteConfigService.Instance.appConfig.config.Value<string>("Str_Win");

        if (looseText != null)
            looseText.text = RemoteConfigService.Instance.appConfig.config.Value<string>("Str_Loose");
    }
}
