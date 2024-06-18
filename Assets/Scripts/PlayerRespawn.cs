using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint;
    private Transform playerTransform;
    private Rigidbody2D rb;
    private SpriteController playerMovement;
    private Animator animator;

    void Start()
    {
        playerTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<SpriteController>();
        animator = GetComponent<Animator>();
    }

    public void Respawn()
    {
        playerMovement.StopMovement();

        playerTransform.position = respawnPoint.position;

        //Resets the velocity and angular velocity
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        //Resets the rotation
        playerTransform.rotation = Quaternion.identity;

        //Re-enables the SpriteController script and allows movement
        playerMovement.enabled = true;
        playerMovement.AllowMovement();

        if (animator != null)
        {
            animator.Play("animIdle", 0, 0f);
        }

        Debug.Log("Player Respawned");
    }

    public void ResetPlayer()
    {
        Respawn();

        Debug.Log("Player state has been reset.");
    }
}
