using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometMovement : MonoBehaviour
{
    public float speed = 5.0f;

    void Update()
    {
        // Move the comet downwards each frame at a constant speed
        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy the comet on collision with any other object
        Destroy(gameObject);
    }
}
