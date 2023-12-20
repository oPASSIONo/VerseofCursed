using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform followTarget;

    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float distance = 5f;

    [SerializeField] float minVerticalAngle = -45;
    [SerializeField] float maxVerticalAngle = 45;

    [SerializeField] private Vector2 framingOffset;
    
    [SerializeField] bool invertX;
    [SerializeField] bool invertY;
    
    float invertValX;
    float invertVAlY;
    
    private float rotationX;
    private float rotationY;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        invertValX = (invertX) ? -1 : 1;
        invertVAlY = (invertY) ? -1 : 1;
        rotationX += Input.GetAxis("Mouse Y") * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        rotationY += Input.GetAxis("Mouse X") * invertValX * rotationSpeed;

        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);

        var focusPosition = followTarget.position + new Vector3(framingOffset.x, framingOffset.y);

        transform.position = followTarget.position - targetRotation * new Vector3(0, 0, distance);
        transform.rotation = targetRotation;
    }

    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY,0);
}
