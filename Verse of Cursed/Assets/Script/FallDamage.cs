using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{
    [Header("Fall System")]
    [SerializeField] private float fallThreshold = 30f;
    [SerializeField] private float fallDamageMultiplier = 2f;

    private CharacterController characterController;
    private float lastYPosition;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        lastYPosition = transform.position.y;
    }

    private void Update()
    {
        bool preview = characterController.isGrounded;
        float currentYPosition = transform.position.y;
        float verticalVelocity = (currentYPosition - lastYPosition) / Time.deltaTime;

        /*if (!characterController.isGrounded && verticalVelocity < -fallThreshold)
        {
            float fallDamage = Mathf.Abs(verticalVelocity) / fallDamageMultiplier;
            Debug.Log(verticalVelocity);
            ApplyFallDamage(fallDamage);
        }*/
        if (!preview && characterController.isGrounded)
        {
            Debug.Log(verticalVelocity < -fallDamageMultiplier);
        }
   
        lastYPosition = currentYPosition;
    }

    private void ApplyFallDamage(float damageAmount)
    {
        // Replace this with your logic to apply fall damage to the player
        Debug.Log("Fall Damage: " + damageAmount);
        // For example, you might reduce the player's health
        // health -= damageAmount;
    }
}