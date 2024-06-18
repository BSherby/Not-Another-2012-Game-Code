using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometMovementTestScript : MonoBehaviour
{
    public float speed = 5.0f;
    public Vector3 animationOffset = new Vector3(0, 0, 0);
    private Animator animator;
    private bool isExploding = false; //Flag to control movement when exploding

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component is not found on the GameObject.");
        }
        transform.position += animationOffset;
        Debug.Log("Initial Position Post-Spawn: " + transform.position);
    }

    private void Update()
    {
        //Only moves the meteor when not in the exploding state
        if (!isExploding)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Tags for collision
        if (other.CompareTag("Ground") || other.CompareTag("SkyTile")|| other.CompareTag("BloodBlock") || other.CompareTag("Player"))
        {
            TriggerExplosion(other.tag);
        }
    }

    private void TriggerExplosion(string tag)
    {
        Debug.Log("Explosion triggered on: " + tag);
        isExploding = true; //Stops the movement
        GetComponent<DynamicColliderAdjustment>()?.StopAdjustments();
        if (animator != null)
        {
            animator.SetTrigger("Explode");
        }

        //Reactions based on the surface tag
        switch(tag)
        {
            case "Ground":
                break;

            case "SkyTile":
                break;

            case "BloodBlock":
                break;

            case "Player":
                break;
        }
        StartCoroutine(DestroyAfterAnimation());
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        CapsuleCollider2D collider = GetComponent<CapsuleCollider2D>();
        if (collider != null)
        {
            Gizmos.color = Color.green;
            Vector3 colliderPosition = transform.position + new Vector3(collider.offset.x, collider.offset.y, 0);

            if (collider.direction == CapsuleDirection2D.Vertical)
                Gizmos.DrawWireCube(colliderPosition, new Vector3(collider.size.x, collider.size.y, 1));
            else
                Gizmos.DrawWireCube(colliderPosition, new Vector3(collider.size.y, collider.size.x, 1));
        }
    }
}
