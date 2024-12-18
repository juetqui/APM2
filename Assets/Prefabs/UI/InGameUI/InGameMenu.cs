using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] Button ResumeBtn;
    [SerializeField] Button RestartBtn;
    [SerializeField] Button MainMenu;
    [SerializeField] UIManager uiManager;
    //[SerializeField] LevelManager levelManager;
    [SerializeField] LevelSelector levelSelector;

    private void Start()
    {
        if (ResumeBtn != null)
        {
            if (levelSelector == LevelSelector.Resume)
                ResumeBtn.onClick.AddListener(ResumeGame);
            else if (levelSelector == LevelSelector.Lvl1)
                ResumeBtn.onClick.AddListener(GoToLvl1);
            else if (levelSelector == LevelSelector.Lvl2)
                ResumeBtn.onClick.AddListener(GoToLvl2);
        }

        RestartBtn.onClick.AddListener(RestartLevel);
        MainMenu.onClick.AddListener(BackToMainMenu);
    }

    private void BackToMainMenu()
    {
        LevelManager.Instance.GoToMainMenu();
    }

    private void RestartLevel()
    {
        LevelManager.Instance.RestartCurrentLevel();
    }

    private void ResumeGame()
    {
        uiManager.SwithToGameplayUI();
    }

    private void GoToLvl1()
    {
        LevelManager.Instance.LoadFirstLevel();
    }

    private void GoToLvl2()
    {
        LevelManager.Instance.LoadSecondLevel();
    }
}

public enum LevelSelector
{
    Resume,
    Lvl1,
    Lvl2
}
