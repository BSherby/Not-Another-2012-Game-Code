using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyTilePlatform : MonoBehaviour
{
    public float minFallDelay = 0.1f;
    public float maxFallDelay = 5.0f;
    private Rigidbody2D rb;
    private Coroutine fallRoutine;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 originalScale;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalScale = transform.localScale;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {        
        if (other.gameObject.CompareTag("Player"))
        {            
            if (fallRoutine == null)
            {
                float delay = Random.Range(minFallDelay, maxFallDelay);
                fallRoutine = StartCoroutine(FallAfterDelay(delay));
            }
        }
        else
        {
            DestroyPlatform();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Cancels the fall if player steps off platform before delay is initiated
            if (fallRoutine != null)
            {
                StopCoroutine(fallRoutine);
                fallRoutine = null;
                Debug.Log("Fall cancelled due to player stepping off the platform");
            }
        }
    }

    IEnumerator FallAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.isKinematic = false;
        rb.gravityScale = 1;
        fallRoutine = null;
        Debug.Log("Platform is falling");
    }

    //Method that destroys the platform
    void DestroyPlatform()
    {
        rb.isKinematic = true;
        gameObject.SetActive(false);
        Debug.Log("Platform destroyed");
    }

    public void ResetPlatform()
    {
        if (fallRoutine != null)
        {
            StopCoroutine(fallRoutine);
            fallRoutine = null;
        }
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        transform.localScale = originalScale;

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;
        rb.gravityScale = 0;
        gameObject.SetActive(true);
        Debug.Log("Platform reset to the original position");
    }
}
