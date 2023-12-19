using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastHit : MonoBehaviour
{
    public float range = 5f;
    void FixedUpdate()
    {
        Vector3 direction = Vector3.forward;
        Ray ray = new Ray(transform.position, transform.TransformDirection(direction * range));
        Debug.DrawRay(transform.position,transform.TransformDirection(direction * range), Color.red);
        UnityEngine.RaycastHit hit;
 
        if (Physics.Raycast(ray, out hit , range))
        {
            Debug.Log("Ray hit: " + hit.collider.gameObject.name);
        }
    }
}
