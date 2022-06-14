using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPGrabber : MonoBehaviour
{
    Ray ray;
    bool isGrabbing;
    
    [SerializeField] Transform hand;

    [SerializeField] Transform interactionRayObject;
    [SerializeField] float lineLength;
    [SerializeField] Camera fpsCamera;

    // [SerializeField] LineRenderer lineRenderer;
    
    void Start()
    {
        // if(lineRenderer == null)
        // {
        //     lineRenderer = GetComponent<LineRenderer>();
        // }
    }

    
    void Update()
    {
        


    }

    void OnRay(InputValue inputValue)
    {
        if(!enabled) return;

        if(inputValue.Get<float>() is 1f)
        {
            isGrabbing = true;
            StartCoroutine(RayRoutine());
        }

        else
        {
            isGrabbing = false;
        }
       

    }



    IEnumerator RayRoutine()
    {
        
        Vector3[] linePositions = new Vector3[2];

        while (isGrabbing)
        {
            ray.origin = hand.position;
            ray.direction = fpsCamera.ScreenPointToRay(Mouse.current.position.ReadValue()).direction;
            hand.right = ray.direction;

            interactionRayObject.transform.position = ray.origin;
            interactionRayObject.forward = ray.direction;

            //lineRenderer.enabled = true;
            // linePositions[0] = ray.origin;
            // linePositions[1] = ray.direction * lineLength;
            // lineRenderer.SetPositions(linePositions);

            //Debug.DrawRay(ray.origin, ray.direction * 20f);


           
            yield return null;
        }

        // lineRenderer.enabled = false;

        yield break;
    }

    bool IsGrabbing()
    {
        return isGrabbing;
    }

    
}
