using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    /*public Transform target;  // The target to follow (player in this case)
    public Vector3 offset = new Vector3(0f, 0, 0);  // Offset from the target

    private void Update()
    {
        transform.position = target.position + offset;
    }*/
    
    public Transform player;  // The player to follow
    public float playerFollowSpeed = 5f;  // Speed of camera following the player

    public float rotationSpeed = 5f;  // Speed of camera rotation with mouse
    public Vector3 offset = new Vector3(0f, 5f, -5f);  // Additional offset from the player

    void LateUpdate()
    {
        if (player != null)
        {
            // Camera follows the player's position with an additional offset
            Vector3 desiredPosition = player.position - player.forward * offset.z + player.up * offset.y + player.right * offset.x;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, playerFollowSpeed * Time.deltaTime);

            // Camera rotates with the mouse
            float mouseX = Input.GetAxis("Mouse X");
            Vector3 rotationAmount = new Vector3(0f, mouseX * rotationSpeed * Time.deltaTime, 0f);
            Quaternion deltaRotation = Quaternion.Euler(rotationAmount);
            transform.rotation = Quaternion.Lerp(transform.rotation, player.rotation * deltaRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
