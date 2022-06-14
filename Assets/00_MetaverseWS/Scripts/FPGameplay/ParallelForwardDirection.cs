using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelForwardDirection : MonoBehaviour
{
    [SerializeField] Transform sourceObject;
   
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.forward = sourceObject.forward;
        transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y, 0 );
    }
}
