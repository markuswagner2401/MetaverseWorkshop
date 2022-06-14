using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealtimeRig : MonoBehaviour
{
    [SerializeField] RigMapping[] rigMappings;

    [System.Serializable]
    public struct RigMapping
    {
        public Transform source;
        public Transform target;
    }

    
    

    
    void Start()
    {
        
    }

   
    void Update()
    {
        foreach (var mapping in rigMappings)
        {
            mapping.target.position = mapping.source.position;
            mapping.target.rotation = mapping.source.rotation;
        }

   
        
    }

    public void SetRigSources(Transform root, Transform head, Transform lefthand, Transform rightHand)
    {
        if (rigMappings.Length < 4)
        {
            print("mapping incomplite, please assign targets for root, head, leftHand, right Hand");
            return;
        }

        rigMappings[0].source = root;
        rigMappings[1].source = head;
        rigMappings[2].source = lefthand;
        rigMappings[3].source = rightHand;
    }

    

   

  
}
