using UnityEngine;

public class PauseService : MonoBehaviour, IFrameworkService
{
    private bool gamePaused = false;

    public void Setup(){}

    public bool GameIsPaused()
    {
        return gamePaused;
    }

    public void PauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        gamePaused = false;
        Time.timeScale = 1;
    }

    public bool TogglePause()
    {
        if (gamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
        return gamePaused;
    }
}
