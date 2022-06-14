using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;

public class RealtimeTransformOwner : MonoBehaviour
{
    RealtimeTransform realtimeTransform;

    private void Awake() 
    {
        realtimeTransform = GetComponent<RealtimeTransform>();
    }
   
    private void Start() 
    {
        RealtimeView realtimeView = GetComponent<RealtimeView>();
        realtimeView.RequestOwnership();
        
        //realtimeTransform.RequestOwnership();
    }

    
}
