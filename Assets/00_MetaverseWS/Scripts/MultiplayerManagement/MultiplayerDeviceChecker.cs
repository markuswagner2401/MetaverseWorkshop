using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

public class MultiplayerDeviceChecker : MonoBehaviour
{
    [Tooltip("VR Rig at Element 0 and FPRig at Element 1")]
    [SerializeField] DeviceRigSources[] deviceRigSources;
    
    [System.Serializable]
    public struct DeviceRigSources
    {
        public string note;
        public Transform root;
        public Transform head;
        public Transform leftHand;
        public Transform rightHand;
        public Transform leftRay;
        public Transform rightRay;
    }

    [SerializeField] UnityEvent onHMDActiveAtStart;
    [SerializeField] UnityEvent onHMDNotActiveAtStart;

    [SerializeField] RealtimeRig realtimeRig;

    GameModes clientsGameMode;

    

    
    
    void Start()
    {
        if(XRSettings.isDeviceActive)
        {
            onHMDActiveAtStart.Invoke();
            
            realtimeRig.SetRigSources(deviceRigSources[0].root, deviceRigSources[0].head, deviceRigSources[0].leftHand, deviceRigSources[0].rightHand);

            SetGameMode(GameModes.vr);

            

            print("hmd acitve");
        }

        else
        {
            onHMDNotActiveAtStart.Invoke();

            realtimeRig.SetRigSources(deviceRigSources[1].root, deviceRigSources[1].head, deviceRigSources[1].leftHand, deviceRigSources[1].rightHand);

           SetGameMode(GameModes.fps);

            print("hmd not active");
        }
    }

    void SetGameMode(GameModes gameMode)
    {
        clientsGameMode = gameMode;
    }

    public GameModes GetClientsGameMode()
    {
        return clientsGameMode;
    }

      

    
    
}
