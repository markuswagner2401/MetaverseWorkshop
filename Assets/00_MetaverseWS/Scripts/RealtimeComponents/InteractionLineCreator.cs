using System;
using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionLineCreator : MonoBehaviour
{
    [SerializeField] Realtime _realtime;
    [SerializeField] string _linePrefabName;
    [SerializeField] LineRenderer _sourceLine;
    [SerializeField] LineSync _lineSync;
    [SerializeField] GameModes gameMode;
    
    [SerializeField] MultiplayerDeviceChecker deviceChecker;
    [SerializeField] bool createAtStart = true;
    bool lineCreated = false;

    bool RTLineEnabled = true;
    GameObject _lineObject;

   
    

    private void Awake() 
    {
        if(_sourceLine == null)
        {
            _sourceLine = GetComponent<LineRenderer>();
        }
        
    }

    void Start()
    {
        if(createAtStart)
        {
            _realtime.didConnectToRoom += DidConnectToRoom;
        }
        
    }

    private void DidConnectToRoom(Realtime realtime)
    {
        if(gameMode != deviceChecker.GetClientsGameMode()) return;
        CreateInteractionLine(_sourceLine);
    }

    void Update()
    {
        if(lineCreated && RTLineEnabled)
        {
            _lineSync.SetLinePoints(_sourceLine.GetPosition(0), _sourceLine.GetPosition(1));
            
        }

       
    }



    

    public void CreateInteractionLine(LineRenderer sourceLine)
    {
        _lineObject = Realtime.Instantiate(prefabName: _linePrefabName, ownedByClient: true, preventOwnershipTakeover: true, useInstance: _realtime);

        _sourceLine = sourceLine;

        _lineSync = _lineObject.GetComponent<LineSync>();

        SetEnabled(false);

        lineCreated = true;

    }

    public void SetEnabled(bool value)
    {
        RTLineEnabled = value;
        if(_lineSync != null)
        {
            _lineSync.SetEnabled(RTLineEnabled);
        }
        
        print("set enabled");
    }

    public void SetColor(Gradient gradient)
    {
        _lineSync.SetColors(gradient.colorKeys[0].color, gradient.colorKeys[1].color, gradient.alphaKeys[0].alpha, gradient.alphaKeys[1].alpha);
    }

    public void DestroyInteractionLine()
    {
        
        Realtime.Destroy(_lineObject);
        lineCreated = false;
    }


}
