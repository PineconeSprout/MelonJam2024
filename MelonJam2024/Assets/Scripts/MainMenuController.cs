using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button startButton;
    public Button levelsButton;
    public Button optionsButton;
    public Button quitButton;
    public Button levelOneButton;
    public Button levelTwoButton;
    public Button levelThreeButton;
    public Button backFromLevelButton;
    public Button backFromOptionsButton;

    public GameObject mainMenuCanvas;
    public GameObject levelsMenuCanvas;
    public GameObject optionsMenuCanvas;

    void Start()
    {
        startButton?.onClick.AddListener(StartGame);
        levelsButton?.onClick.AddListener(OpenLevelsMenu);
        optionsButton?.onClick.AddListener(OpenOptionsMenu);
        quitButton?.onClick.AddListener(QuitGame);
        levelOneButton?.onClick.AddListener(StartGame);
        levelTwoButton?.onClick.AddListener(LevelTwoStart);
        levelThreeButton?.onClick.AddListener(LevelThreeStart);
        backFromLevelButton?.onClick.AddListener(BackToMainMenu);
        backFromOptionsButton?.onClick.AddListener(BackToMainMenu);

        //Ensure all menus are disabled at the start
        levelsMenuCanvas?.SetActive(false);
        optionsMenuCanvas?.SetActive(false);
    }

    void StartGame()
    {
        SceneManager.LoadScene("FirstLevel");
    }

    void LevelTwoStart()
    {
        SceneManager.LoadScene("SecondLevel");
    }

    void LevelThreeStart()
    {
        SceneManager.LoadScene("ThirdLevel");
    }

    void OpenLevelsMenu()
    {
        mainMenuCanvas?.SetActive(false);
        levelsMenuCanvas?.SetActive(true);
    }

    void OpenOptionsMenu()
    {
        mainMenuCanvas?.SetActive(false);
        optionsMenuCanvas?.SetActive(true);
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void BackToMainMenu()
    {
        levelsMenuCanvas?.SetActive(false);
        optionsMenuCanvas?.SetActive(false);
        mainMenuCanvas?.SetActive(true);
    }
}
