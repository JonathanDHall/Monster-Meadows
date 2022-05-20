using UnityEngine;

public class Pause : MonoBehaviour
{
    bool _isPaused;
    [SerializeField] private GameObject _pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(!_isPaused);
        }
    }

    public void PauseGame(bool pause)
    {
        _isPaused = pause;
        switch (pause)
        {
            case true:
                Time.timeScale = 0;
                _pauseMenu.SetActive(true);
                break;
            case false:
                Time.timeScale = 1;
                _pauseMenu.SetActive(false);
                break;
        }
    }
}