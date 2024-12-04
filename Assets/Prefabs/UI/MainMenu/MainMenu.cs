using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button StartBtn;
    [SerializeField] Button ControlsBtn;
    [SerializeField] Button BackBtn;
    [SerializeField] Button BackBtnOptions;
    [SerializeField] Button OptionsBtn;
    [SerializeField] CanvasGroup FrontUI;
    [SerializeField] CanvasGroup ControllsUI;
    [SerializeField] CanvasGroup OptionsUI;
    [SerializeField] LevelManager levelManager;
    [SerializeField] StaminaSystem staminaSystem;
    [SerializeField] PlayerPrefsController prefs;
    [SerializeField] TextMeshProUGUI currencyText;

    private void Start()
    {
        StartBtn.onClick.AddListener(StartGame);
        ControlsBtn.onClick.AddListener(SwithToControlUI);
        OptionsBtn.onClick.AddListener(SwithToOptionsUI);
        BackBtn.onClick.AddListener(SwitchToFrontUI);
        BackBtnOptions.onClick.AddListener(SwitchToFrontUI);

        prefs.onSavedPrefs += SetCurrencyText;

        SetCurrencyText(prefs.Load());
    }

    private void SwitchToFrontUI()
    {
        ControllsUI.blocksRaycasts = false;
        ControllsUI.alpha = 0;

        OptionsUI.blocksRaycasts = false;
        OptionsUI.alpha = 0;

        FrontUI.blocksRaycasts = true;
        FrontUI.alpha = 1;
    }

    private void SwithToControlUI()
    {
        ControllsUI.blocksRaycasts = true;
        ControllsUI.alpha = 1;

        FrontUI.blocksRaycasts = false;
        FrontUI.alpha = 0;
    }

    private void SwithToOptionsUI()
    {
        OptionsUI.blocksRaycasts = true;
        OptionsUI.alpha = 1;

        FrontUI.blocksRaycasts = false;
        FrontUI.alpha = 0;
    }

    private void StartGame()
    {
        if (staminaSystem.UseStamina(3)) levelManager.LoadFirstLevel();
    }

    private void SetCurrencyText(int myCurrency)
    {
        currencyText.text = "$" + myCurrency.ToString();
    }
}
