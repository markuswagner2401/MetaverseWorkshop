using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabColliderCreator : MonoBehaviour
{
    MeshFilter[] meshFilters;
    MeshCollider[] grabColliders;

    List<Collider> grabColliderList;

    XROffsetInteractable offsetInteractable;
    void Start()
    {
        meshFilters = GetComponentsInChildren<MeshFilter>();

        grabColliders = new MeshCollider[meshFilters.Length];

        for (int i = 0; i < grabColliders.Length; i++)
        {
            grabColliders[i] = new MeshCollider();
            grabColliders[i].sharedMesh = meshFilters[i].mesh;
            
        }

        offsetInteractable = GetComponent<XROffsetInteractable>();

        

        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
