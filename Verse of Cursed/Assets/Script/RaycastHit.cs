using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastHit : MonoBehaviour
{
    public float range = 5f;
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject sourceObject;

    private void Awake()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RayShoot();

        }
    }

    public void RayShoot()
    {
        Vector3 direction = Vector3.forward;
        Ray ray = new Ray(transform.position, transform.TransformDirection(direction * range));
        Debug.DrawRay(transform.position, transform.TransformDirection(direction * range), Color.red);
        UnityEngine.RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                PullMesh();
                Debug.Log("Ray hit: " + hit.collider.gameObject.name);
            }
        }
    }
    
    public void PullMesh()
    {
        MeshFilter sourceMeshFilter = sourceObject.GetComponent<MeshFilter>();
        MeshFilter playerMeshFilter = playerObject.GetComponent<MeshFilter>();

        if (sourceMeshFilter != null && playerMeshFilter != null)
        {
            // Copy the mesh from sourceObject to playerObject
            playerMeshFilter.mesh = sourceMeshFilter.mesh;
        }
        else
        {
            Debug.LogError("MeshFilter not found on sourceObject or playerObject.");
        }
    }
    

}



