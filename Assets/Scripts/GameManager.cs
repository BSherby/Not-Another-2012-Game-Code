using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerRespawn playerRespawn;
    public SkyTilePlatform[] platforms;
    public Transform player;
    public Transform checkpoint;
    public float deathPauseDelay = 0.5f;

    private int deathCount = 0;
    private const int MaxDeaths = 3;
    private Animator playerAnimator;
    private bool isGameOver = false;
    private bool isRespawning = false;

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
        InitializeGame();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            InitializeGame();
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            if (isGameOver && Input.GetKeyDown(KeyCode.R))
            {
                RespawnPlayer();
            }

            if (player && checkpoint)
            {
                if (Vector3.Distance(player.position, checkpoint.position) < 0.5f)
                {
                    SceneTransitionManager.Instance.TransitionToGameCompletion();
                }
            }
        }
    }

    public void PlayerDied()
    {
        if (isRespawning) return;

        deathCount++;
        Debug.Log("PlayerDied() called. Current death count: " + deathCount);

        if (deathCount % MaxDeaths == 0)
        {
            ShowDeathCard();
        }
        else
        {
            StartCoroutine(HandlePlayerDeath());
        }
    }

    private void ShowDeathCard()
    {
        Debug.Log("Showing random death card");
        DeathCardManager.Instance.ShowRandomDeathCard();
        Time.timeScale = 0f;
    }

    private IEnumerator HandlePlayerDeath()
    {
        isRespawning = true;
        yield return new WaitForSeconds(deathPauseDelay);

        UIManager.Instance.ShowRespawnText();
        isGameOver = true;
        Time.timeScale = 0f;
    }

    public void RespawnPlayer()
    {
        isRespawning = false;

        if (playerRespawn == null)
        {
            playerRespawn = Object.FindAnyObjectByType<PlayerRespawn>();
        }

        if (playerAnimator == null && playerRespawn != null)
        {
            playerAnimator = playerRespawn.GetComponent<Animator>();
        }

        if (playerRespawn != null && playerAnimator != null)
        {
            playerRespawn.Respawn();
            playerAnimator.SetTrigger("Respawn");

            foreach (var platform in platforms)
            {
                platform.ResetPlatform();
            }

            PlayerDeath playerDeath = player.GetComponent<PlayerDeath>();
            if (playerDeath != null)
            {
                playerDeath.ResetDeathFlag();
            }

            UIManager.Instance.HideRespawnText();
            DeathCardManager.Instance.HideDeathCard();
            isGameOver = false;
            Time.timeScale = 1f;
            Debug.Log("Player respawned and all platforms have been reset to their initial position. Current death count: " + deathCount);
        }
        else
        {
            Debug.LogError("PlayerRespawn or PlayerAnimator is not assigned.");
        }
    }

    public void ResetDeathCount()
    {
        deathCount = 0;
        Debug.Log("Death count reset.");
    }

    public void InitializeGame()
    {
        Debug.Log("Initializing Game. Current death count: " + deathCount);

        playerRespawn = Object.FindAnyObjectByType<PlayerRespawn>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        checkpoint = GameObject.FindGameObjectWithTag("Checkpoint").transform;

        if (playerRespawn != null)
        {
            playerAnimator = playerRespawn.GetComponent<Animator>();
            if (playerAnimator == null)
            {
                Debug.LogError("Animator component is not found on the PlayerRespawn GameObject.");
            }
        }
        else
        {
            Debug.LogError("PlayerRespawn is not assigned in the GameManager.");
        }

        platforms = GameObject.FindGameObjectsWithTag("SkyTile").Select(go => go.GetComponent<SkyTilePlatform>()).ToArray();

        if (UIManager.Instance != null)
        {
            UIManager.Instance.InitializeUI();
            UIManager.Instance.HideRespawnText();
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.InitializeAudio();
            AudioManager.Instance.PlayRandomMusic();
        }

        if (DeathCardManager.Instance != null)
        {
            DeathCardManager.Instance.InitializeDeathCardManager();
        }

        ResetDeathCount();
        Debug.Log("Game initialized.");
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void RestartGame()
    {
        SceneTransitionManager.Instance.RestartGame();
    }

    public void EndGame()
    {
        SceneTransitionManager.Instance.TransitionToMainMenu();
    }

    public void TransitionToStartScreen()
    {
        SceneTransitionManager.Instance.TransitionToStartScreen();
    }
}
