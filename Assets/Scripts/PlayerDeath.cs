using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private SpriteController playerMovement;
    private Animator animator;
    private bool isDying = false;

    private void Start()
    {
        playerMovement = GetComponent<SpriteController>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDying) return;

        if (collision.gameObject.CompareTag("BloodBlock"))
        {
            Debug.Log("Collided with BloodBlock");
            Die();
        }
        else if (collision.gameObject.CompareTag("Meteor"))
        {
            Debug.Log("Collided with Meteor");
            Die2();
        }
    }

    public void Die()
    {
        if (isDying) return;
        isDying = true;

        Debug.Log("Player has died!");
        if (animator != null)
        {
            animator.SetTrigger("Die");
            Debug.Log("Die trigger set");
        }
        else
        {
            Debug.LogError("Animator component is missing.");
        }
        if (playerMovement != null)
        {
            playerMovement.StopMovement();
            playerMovement.enabled = false;
        }
        Debug.Log("Calling GameManager.PlayerDied from Die()");
        GameManager.Instance.PlayerDied();
    }

    public void Die2()
    {
        if (isDying) return;
        isDying = true;

        Debug.Log("Player has died with ExplosiveDeath animation");
        if (animator != null)
        {
            animator.SetTrigger("Die2");
            Debug.Log("Die2 trigger set");
        }
        else
        {
            Debug.LogError("Animator component is missing.");
        }
        if (playerMovement != null)
        {
            playerMovement.StopMovement();
            playerMovement.enabled = false;
        }
        Debug.Log("Calling GameManager.PlayerDied from Die2()");
        GameManager.Instance.PlayerDied();
    }

    public void ResetDeathFlag()
    {
        isDying = false;
        Debug.Log("Death flag reset");
    }
}