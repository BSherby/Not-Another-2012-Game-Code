using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    public void FollowPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 newPosition = player.position;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }
    }
}
