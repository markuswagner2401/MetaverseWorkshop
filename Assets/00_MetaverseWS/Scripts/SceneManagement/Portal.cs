using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;
using UnityEngine.Events;

public class Portal : MonoBehaviour
{
    [SerializeField] UnityEvent eventOnCollsision;

    

    private void OnTriggerEnter(Collider other) 
    {
        print("szenenwechsel collision");

        if(other.gameObject.tag == "Player")
        {
            print("collsion with player");
            eventOnCollsision.Invoke();
        }

        
        
        
    }
    
}
