using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TMP_Text respawnText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeUI();
    }

    public void ResetUI()
    {
        if (respawnText != null)
        {
            respawnText.gameObject.SetActive(false);
        }

        Debug.Log("UI has been reset.");
    }

    public void InitializeUI()
    {
        if (respawnText == null)
        {
            respawnText = Object.FindAnyObjectByType<TMP_Text>();
        }

        if (respawnText != null)
        {
            respawnText.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("RespawnText is not assigned or found in the scene.");
        }

        Debug.Log("UI initialized.");
    }

    public void ShowRespawnText()
    {
        if (respawnText != null)
        {
            respawnText.gameObject.SetActive(true);
        }
    }

    public void HideRespawnText()
    {
        if (respawnText != null)
        {
            respawnText.gameObject.SetActive(false);
        }
    }
}
