using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardVector : MonoBehaviour
{
   
    [SerializeField] Transform source;
    [SerializeField] float smoothing = 0.01f;

    Vector3 lastForward = new Vector3();
    Vector3 smoothedForward = new Vector3();
    Ray ray;

    void Start()
    {
        lastForward = transform.forward;
    }


    void Update()
    {
        if (ReferenceEquals(source, null)) return;


      
        transform.position = source.position;
        transform.forward = source.forward;
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
       

       
        



        smoothedForward = Vector3.Lerp(transform.forward, lastForward, smoothing);

        lastForward = transform.forward;
    }

    public Vector3 GetOnFloorForward()
    {
        return transform.forward;
    }

    public Vector3 GetSmoothedOnFloorForward()
    {
        return smoothedForward;
    }

    public Quaternion GetOnFloorRotation()
    {
        return transform.rotation;
    }
}
