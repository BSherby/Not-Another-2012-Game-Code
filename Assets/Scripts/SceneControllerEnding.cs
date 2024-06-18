using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControllerEnding : MonoBehaviour
{
    public string nextSceneName = "Start Screen";
    public float transitionTime = 90f;
    public AudioSource audioSource;

    private bool sceneEnded = false;

    void Start()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Audio source is not assigned/working");
        }
        StartCoroutine(AutoTransition());
    }

    private IEnumerator AutoTransition()
    {
        yield return new WaitForSeconds(transitionTime);

        if (!sceneEnded)
        {
            sceneEnded = true;
            TransitionToNextScene();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!sceneEnded)
            {
                sceneEnded = true;
                TransitionToNextScene();
            }
        }
    }

    private void TransitionToNextScene()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        Debug.Log("Transitioning to start screen from game completion");
        SceneTransitionManager.Instance.TransitionToStartScreen();
    }
}
