using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Text levelText;

    private void Start()
    {
        if (levelText != null)
        {
            int lvl = PlayerPrefs.GetInt("Level", 0);
            levelText.text = "Level: " + lvl;
        }
        else
        {
            throw new System.Exception("Level text is null");
        }
    }

    public void StartGame()
    {
        SceneLoader.LoadScene(1);
    }

    public void RestartGame()
    {
        PlayerPrefs.DeleteKey("Level");
        StartGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
