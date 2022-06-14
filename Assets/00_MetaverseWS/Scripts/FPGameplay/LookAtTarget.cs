using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] Transform headRotation;
    [SerializeField] Transform headPosition;
    [SerializeField] float offset;
    
    void Start()
    {
        
    }

   
    void Update()
    {
        
        transform.position = headPosition.position + headRotation.forward * offset;
        
    }
}
