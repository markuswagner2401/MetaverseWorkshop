using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falle : MonoBehaviour
{
    [SerializeField] Rigidbody boden;
    [SerializeField] string triggerTag;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag != triggerTag) return;
        
        BodenFaellt();
    }

    private void BodenFaellt()
    {
        
        boden.useGravity = true;
    }
}
