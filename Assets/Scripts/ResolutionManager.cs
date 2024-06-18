using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResolutionManager : MonoBehaviour
{
    public static ResolutionManager Instance;

    public int targetWidth = 1920;
    public int targetHeight = 1080;
    public FullScreenMode fullScreenMode = FullScreenMode.FullScreenWindow;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SetResolution();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetResolution();
    }

    void SetResolution()
    {
        Screen.SetResolution(targetWidth, targetHeight, fullScreenMode);
        Debug.Log("Resolution set to: " + targetWidth + "x" + targetHeight + " in " + fullScreenMode + " mode.");
    }
}
