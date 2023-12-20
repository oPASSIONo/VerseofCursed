using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 500f;

    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private Vector3 groundCheckOffset;
    [SerializeField] private LayerMask groundLayer;

    private bool isGrounded;

    private Quaternion targetRotation;

    private CameraFollow cameraController;
    private CharacterController characterController;

    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraFollow>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float moveAmount = Mathf.Abs(horizontal) + Mathf.Abs(vertical);

        var moveInput = (new Vector3(horizontal, 0, vertical)).normalized;

        var moveDir =  cameraController.PlanarRotation * moveInput;
        
       GroundCheck();
       Debug.Log("Isground = " + isGrounded);

        if (moveAmount > 0)
        {
            characterController.Move(moveDir * moveSpeed * Time.deltaTime);
            transform.position += moveInput * moveSpeed * Time.deltaTime;
            targetRotation = transform.rotation = Quaternion.LookRotation(moveDir);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    
    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }
}




