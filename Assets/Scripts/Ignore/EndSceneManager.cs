using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndSceneManager : MonoBehaviour
{
    public GameObject[] endCards;
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public float autoReturnTime = 30f;

    // Start is called before the first frame update
    void Start()
    {
        if (audioSource1 == null || audioSource2 == null)
        {
            Debug.LogError("Audio sources are not assigned/working");
            return;
        }

        audioSource1.Play();
        audioSource2.Play();

        DisplayRandomEndCard();

        StartCoroutine(AutoReturnToGame());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReturnToGame();
        }
    }

    private void DisplayRandomEndCard()
    {
        if (endCards.Length == 0)
        {
            Debug.LogError("No end cards assigned in the manager");
            return;
        }

        foreach (var card in endCards)
        {
            card.SetActive(false);
        }

        //Selects a random end card to display
        int randomIndex = Random.Range(0, endCards.Length);
        endCards[randomIndex].SetActive(true);
    }

    private IEnumerator AutoReturnToGame()
    {
        yield return new WaitForSeconds(autoReturnTime);
        ReturnToGame();
    }

    private void ReturnToGame()
    {
        audioSource1.Stop();
        audioSource2.Stop();

        // Reset the death count in the GameManager
        GameManager.Instance.ResetDeathCount();

        SceneManager.LoadScene("Game");
    }
}
