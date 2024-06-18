using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderTest : MonoBehaviour
{
    public string testSceneName = "Start Screen"; // Ensure this matches exactly the name in the Build Settings

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) // Press 'L' to load the scene
        {
            Debug.Log($"Loading test scene: {testSceneName}");
            SceneManager.LoadScene(testSceneName);
        }
    }
}
