using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        var moveInput = (new Vector3(horizontal, 0, vertical)).normalized;

        transform.position += moveInput * moveSpeed * Time.deltaTime;
    }
}




