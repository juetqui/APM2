using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//[CreateAssetMenu(menuName ="LevelManager")]
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] int MainMenuBuildIndex = 0;
    [SerializeField] int TutorialBuildIndex = 1;
    [SerializeField] int FirstLevelBuildIndex = 2;
    [SerializeField] int SecondLevelBuildIndex = 3;

    [SerializeField] Canvas LoadingScreen;
    [SerializeField] Image LoadingBar;

    public delegate void OnLevelFinished();
    public event OnLevelFinished onLevelFinished;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void LevelFinished()
    {
        onLevelFinished?.Invoke();
    }

    public void GoToMainMenu()
    {
        LoadSceneByIndex(MainMenuBuildIndex);
    }

    public void LoadTutorial()
    {
        LoadSceneByIndex(TutorialBuildIndex);
    }

    public void LoadFirstLevel()
    {
        LoadSceneByIndex(FirstLevelBuildIndex);
    }

    public void LoadSecondLevel()
    {
        LoadSceneByIndex(SecondLevelBuildIndex);
    }

    public void RestartCurrentLevel()
    {
        LoadSceneByIndex(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadSceneByIndex(int index)
    {
        AsyncOperation aop = SceneManager.LoadSceneAsync(index);
        StartCoroutine(Load(aop));
    }

    private IEnumerator Load(AsyncOperation aop)
    {
        GameplayStatics.SetGamePaused(true);
        LoadingScreen.gameObject.SetActive(true);
        LoadingBar.fillAmount = 0;

        while (!aop.isDone)
        {
            LoadingBar.fillAmount = aop.progress;
            yield return null;
        }

        LoadingScreen.gameObject.SetActive(false);
        GameplayStatics.SetGamePaused(false);
    }
}
