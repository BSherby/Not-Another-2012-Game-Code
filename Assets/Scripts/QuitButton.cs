using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public AudioSource audioSource;
    public float delay = 1f;

    public void QuitGame()
    {
        if (audioSource != null)
        {
            audioSource.Play();
            StartCoroutine(WaitAndQuit(audioSource.clip.length));
        }
        else
        {
            QuitImmediately();
        }
    }

    private IEnumerator WaitAndQuit(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        QuitImmediately();
    }

    private void QuitImmediately()
    {
        Debug.Log("Quitting game...");
        Application.Quit();

        // If running in the editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
