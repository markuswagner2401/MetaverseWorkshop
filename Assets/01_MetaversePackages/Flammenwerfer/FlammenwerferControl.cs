using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Normal.Realtime;
using System;

public class FlammenwerferControl : MonoBehaviour
{
    [SerializeField] InputActionReference particleControlInputRef;

    [SerializeField] GameModes gameMode;

    [SerializeField] MultiplayerDeviceChecker deviceChecker;

    [SerializeField] string prefabName;

    [SerializeField] Transform handTransform;

    [SerializeField] Realtime realtime;
    ParticleSync particleSync;

    GameObject particleObject;

    bool particleObjectCreated;
   
    void Start()
    {
        realtime.didConnectToRoom += DidConnectToRoom;
    }

    private void DidConnectToRoom(Realtime realtime)
    {
        if(deviceChecker != null)
        {
            if(gameMode != deviceChecker.GetClientsGameMode()) return;
        }
        

        CreateFlameThrower();
    }

    void Update()
    {
        if(!particleObjectCreated) return;


        if(particleControlInputRef.action.ReadValue<float>() > 0f)
        {
            
            particleSync.SetEmissionModel(true);
        }

        else
        {
            particleSync.SetEmissionModel(false);
        }

        particleObject.transform.position = handTransform.transform.position;
        particleObject.transform.rotation = handTransform.transform.rotation;
        
    }

    

    public void CreateFlameThrower()
    {
        particleObject = Realtime.Instantiate(prefabName, ownedByClient: true, preventOwnershipTakeover: true, useInstance: realtime);

        particleSync = particleObject.GetComponent<ParticleSync>();

        particleObject.GetComponent<RealtimeTransform>().RequestOwnership();

        particleObjectCreated = true;
        
    }


}
