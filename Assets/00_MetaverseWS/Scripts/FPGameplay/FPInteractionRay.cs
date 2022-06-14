using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class FPInteractionRay : MonoBehaviour
{
    [SerializeField] Transform hand;
    [SerializeField] Camera fpCam;

    //[SerializeField] LineRenderer lineRenderer;
    
    [SerializeField] float lineLength = 100f;

    [SerializeField] LayerMask raycastMask;


    Ray ray = new Ray();

    Vector3[] points;

    bool isGrabbing = false;
    
    



    void Start()
    {
        points = new Vector3[2];

        // if(lineRenderer == null)
        // {
        //     lineRenderer = GetComponent<LineRenderer>();
        // }
    }

    
    void Update()
    {
        transform.position = hand.transform.position;
        //transform.forward = fpCam.ScreenPointToRay(Mouse.current.position.ReadValue()).direction;

        transform.forward = fpCam.transform.forward;

        ray.origin = transform.position;
        ray.direction = transform.forward;

        points[0] = ray.origin;


    }

    
}
