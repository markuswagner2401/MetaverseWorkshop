using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Camera cam;

    [SerializeField] bool lookAtCamera;
    Vector3 directionToCamera = new Vector3();
    
    void Start()
    {
        cam = Camera.main;
    }

    
    void Update()
    {
        if(lookAtCamera)
        {
            if(cam == null) return;
            directionToCamera = (cam.transform.position - transform.position).normalized;
            transform.forward = -directionToCamera;
        }
    }
}
