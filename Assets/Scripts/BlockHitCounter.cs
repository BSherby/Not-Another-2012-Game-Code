using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlockHitCounter : MonoBehaviour
{
    public int requiredHits = 10;
    public Image displayImage;
    public float displayDuration = 2f;
    private AudioSource hitSound1;
    private AudioSource hitSound2;
    private int hitCount = 0;

    void Start()
    {
        if (displayImage != null)
        {
            displayImage.enabled = false;
        }

        hitSound1 = GameObject.FindWithTag("HitBlockSFX1")?.GetComponent<AudioSource>();
        hitSound2 = GameObject.FindWithTag("HitBlockSFX2")?.GetComponent<AudioSource>();

        if (hitSound1 == null || hitSound2 == null)
        {
            Debug.LogError("HitBlockSFX1 or HitBlockSFX2 AudioSource is not assigned or found in the scene.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hitCount++;
            PlayHitSound();

            if (hitCount >= requiredHits)
            {
                StartCoroutine(DisplayImage());
                hitCount = 0;
            }
        }
    }

    private void PlayHitSound()
    {
        if (hitSound1 != null && !hitSound1.isPlaying)
        {
            hitSound1.Play();
        }
        else if (hitSound2 != null && !hitSound2.isPlaying)
        {
            hitSound2.Play();
        }
    }

    private IEnumerator DisplayImage()
    {
        if (displayImage != null)
        {
            displayImage.enabled = true;
            yield return new WaitForSeconds(displayDuration);
            displayImage.enabled = false;
        }
    }
}
