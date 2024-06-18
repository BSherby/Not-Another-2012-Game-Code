using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControllerOpening : MonoBehaviour
{
    public string nextSceneName;
    public float transitionTime = 5f;
    public string skipButton = "Jump";
    public AudioSource audioSource;

    private bool sceneEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Audio Source is not assigned in the SceneController.");
        }

        StartCoroutine(AutoTransition());
    }

    // Update is called once per frame
    void Update()
    {
        if (!sceneEnded && Input.GetButton(skipButton))
        {
            sceneEnded = true;
            TransitionToNextScene();
        }
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

    private void TransitionToNextScene()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        PlayerPrefs.SetInt("FromIntroStory", 1);
        SceneManager.LoadScene(nextSceneName);
    }
}
