using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastHit : MonoBehaviour
{
    public float range = 5f;
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject sourceObject;
    [SerializeField] GameObject sourceObject1;

    private void Awake()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RayShoot();

        }
        
        RayShoot();

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
            }
            if (hit.collider.CompareTag("Enemy1"))
            {
                PullMesh1();
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
            playerObject.transform.localScale = new Vector3(1f, 0.7f, 1f);

        }
        else
        {
            Debug.LogError("MeshFilter not found on sourceObject or playerObject.");
        }
    }
    
    public void PullMesh1()
    {
        MeshFilter sourceMeshFilter1 = sourceObject1.GetComponent<MeshFilter>();
        MeshFilter playerMeshFilter = playerObject.GetComponent<MeshFilter>();
        
        if (sourceMeshFilter1 != null && playerMeshFilter != null)
        {
            // Copy the mesh from sourceObject to playerObject
            playerMeshFilter.mesh = sourceMeshFilter1.mesh;
            playerObject.transform.localScale = new Vector3(1f, 0.6f, 1f);
        }
        else
        {
            Debug.LogError("MeshFilter not found on sourceObject or playerObject.");
        }
    }
    

}



