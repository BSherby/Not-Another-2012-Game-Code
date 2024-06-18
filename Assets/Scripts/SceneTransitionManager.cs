using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    private bool shouldResetGame = false;

    private void Awake()
    {
        Debug.Log("SceneTransitionManager Awake called.");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("SceneTransitionManager instance created and set to DontDestroyOnLoad.");
        }
        else if (Instance != this)
        {
            Debug.Log("Another instance of SceneTransitionManager exists, destroying this one.");
            Destroy(gameObject);
        }
    }

    public void TransitionToScene(string sceneName, float delay = 0f)
    {
        StartCoroutine(Transition(sceneName, delay));
    }

    private IEnumerator Transition(string sceneName, float delay)
    {
        if (delay > 0f)
        {
            yield return new WaitForSeconds(delay);
        }

        Debug.Log("Transitioning to scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
        ReinitializeManagers();
    }

    public void TransitionToGameCompletion()
    {
        SceneManager.LoadScene("Game Completion");
    }

    public void TransitionToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        shouldResetGame = true;
        Debug.Log("Restarting game. Reset flag set to true.");
        SceneManager.LoadScene("Game");
    }

    public void TransitionToStartScreen()
    {
        shouldResetGame = true;
        Debug.Log("Transitioning to start screen. Reset flag set to true.");
        SceneManager.LoadScene("Start Screen");
    }

    public void StartGame()
    {
        if (shouldResetGame)
        {
            shouldResetGame = false;
            Debug.Log("Starting game. Reset flag set to false. Loading Game scene.");
            SceneManager.LoadScene("Game");
        }
        else
        {
            Debug.Log("Starting game normally.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public bool ShouldResetGame()
    {
        Debug.Log("Checking reset flag: " + shouldResetGame);
        return shouldResetGame;
    }

    private void ReinitializeManagers()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.InitializeGame();
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.InitializeAudio();
            AudioManager.Instance.PlayRandomMusic();
        }

        if (UIManager.Instance != null)
        {
            UIManager.Instance.InitializeUI();
        }
    }
}