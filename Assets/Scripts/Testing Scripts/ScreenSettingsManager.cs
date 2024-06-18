using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenSettingsManager : MonoBehaviour
{
    public static ScreenSettingsManager Instance;

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
    }

    void Start()
    {
        SetFullScreenMode();
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
        SetFullScreenMode();
        Debug.Log("Scene loaded: " + scene.name + ". Full-screen mode set.");
    }

    public void SetFullScreenMode()
    {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow; // or FullScreenMode.ExclusiveFullScreen
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        Debug.Log("Full-screen mode set. Resolution: " + Screen.currentResolution.width + "x" + Screen.currentResolution.height);
    }
}
