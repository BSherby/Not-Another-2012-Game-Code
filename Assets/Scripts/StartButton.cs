using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public AudioSource audioSource;
    public float delay = 1f;

    public void PlayGame()
    {
        Debug.Log("PlayGame called.");
        if (audioSource != null)
        {
            audioSource.Play();
            StartCoroutine(WaitAndStartGame(audioSource.clip.length));
        }
        else
        {
            StartGameAfterDelay();
        }
    }

    private IEnumerator WaitAndStartGame(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        StartGameAfterDelay();
    }

    private void StartGameAfterDelay()
    {
        Debug.Log("StartGameAfterDelay called.");
        if (SceneTransitionManager.Instance == null)
        {
            Debug.LogError("SceneTransitionManager.Instance is null in StartButton.");
            return;
        }

        if (SceneTransitionManager.Instance.ShouldResetGame())
        {
            Debug.Log("Reset flag is true. Reloading Game scene.");
            SceneTransitionManager.Instance.StartGame();
        }
        else
        {
            Debug.Log("Reset flag is false. Loading next scene in build index.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
