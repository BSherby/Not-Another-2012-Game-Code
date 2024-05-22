using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private SpriteController playerMovement;
    private Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        //Link to the movement script
        playerMovement = GetComponent<SpriteController>();
        animator = GetComponent<Animator>();
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Deadly")
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

    }
}
