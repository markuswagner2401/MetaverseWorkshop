using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class XRRayInteractionPlus : XRRayInteractor
{
    [SerializeField] InputActionReference rayInputActionRef;

    [SerializeField] UnityEvent onRay;
    bool onRayFired = false;
    [SerializeField] UnityEvent onRayFinish;
    bool onRayFinishedFired = false;

    private void Update() 
    {
        if(rayInputActionRef.action.ReadValue<float>() == 1f)
        {
            onRayFinishedFired = false;
            if(onRayFired) return;
            onRayFired = true;
            onRay.Invoke();
        }

        else
        {
            onRayFired = false;
            if(onRayFinishedFired) return;
            onRayFinishedFired = true;
            onRayFinish.Invoke();
        }
    }
}


