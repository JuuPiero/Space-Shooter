using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float worldSpeed = 2f;

    public event Action OnGamePaused;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }

    public void Pause()
    {
        OnGamePaused?.Invoke();
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
