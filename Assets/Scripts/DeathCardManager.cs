using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCardManager : MonoBehaviour
{
    public static DeathCardManager Instance;

    public GameObject[] deathCards;
    public AudioSource audioSource1;
    public AudioSource audioSource2;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeDeathCardManager();
    }

    public void ShowRandomDeathCard()
    {
        if (deathCards.Length == 0)
        {
            Debug.LogError("No death cards assigned in the manager");
            return;
        }

        foreach (var card in deathCards)
        {
            card.SetActive(false);
        }

        int randomIndex = Random.Range(0, deathCards.Length);
        deathCards[randomIndex].SetActive(true);

        if (audioSource1 != null) audioSource1.Play();
        if (audioSource2 != null)  audioSource2.Play();

        AudioManager.Instance.StopMusic();
        Time.timeScale = 0f;
        StartCoroutine(CheckForRespawnKey());
    }

    private IEnumerator CheckForRespawnKey()
    {
        while (!Input.GetKeyDown(KeyCode.R))
        {
            yield return null;
        }
        RespawnPlayer();
    }

    public void HideDeathCard()
    {
        foreach (var card in deathCards)
        {
            card.SetActive(false);
        }

        if (audioSource1 != null)  audioSource1.Stop();
        if (audioSource2 != null)  audioSource2.Stop();
        Time.timeScale = 1f;
    }

    private void RespawnPlayer()
    {
        GameManager.Instance.ResetDeathCount();
        GameManager.Instance.RespawnPlayer();
        HideDeathCard();
        AudioManager.Instance.ResumeMusic();
    }

    public void InitializeDeathCardManager()
    {
        if (audioSource1 == null || audioSource2 == null)
        {
            audioSource1 = GameObject.Find("AudioSource1").GetComponent<AudioSource>();
            audioSource2 = GameObject.Find("AudioSource2").GetComponent<AudioSource>();
        }

        if (audioSource1 == null || audioSource2 == null)
        {
            Debug.LogError("AudioSource components are not assigned or found in the scene.");
        }

        deathCards = GameObject.FindGameObjectsWithTag("DeathCard");

        if (deathCards == null || deathCards.Length == 0)
        {
            List<GameObject> foundCards = new List<GameObject>();
            GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                if (obj.CompareTag("DeathCard"))
                {
                    foundCards.Add(obj);
                    obj.SetActive(false);
                }
            }
            deathCards = foundCards.ToArray();
        }
        if (deathCards == null || deathCards.Length == 0)
        {
            Debug.LogError("Death cards are not assigned in the manager.");
        }

        Debug.Log("DeathCardManager initialized");
    }
}
