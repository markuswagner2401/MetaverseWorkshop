using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrab : XRGrabInteractable
{
    Vector3 interactorPosition = Vector3.zero;
    Quaternion interactorRotation = Quaternion.identity;

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        StoreInteractor(args.interactor);
        MatchAttachmentPoints(args.interactor);
        
    }

    private void StoreInteractor(XRBaseInteractor interactor)
    {
        interactorPosition = interactor.attachTransform.localPosition;
        interactorRotation = interactor.attachTransform.localRotation;
    }

    private void MatchAttachmentPoints(XRBaseInteractor interactor)
    {
        bool hasAttach = attachTransform != null;
        interactor.attachTransform.position = hasAttach? attachTransform.position : transform.position;
        interactor.attachTransform.rotation = hasAttach? attachTransform.rotation : transform.rotation;
    }



    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
        ResetAttachmentPoint(args.interactor);
        ClearInteractor(args.interactor);

    }

    private void ResetAttachmentPoint(XRBaseInteractor interactor)
    {
        interactor.attachTransform.localPosition = interactorPosition;
        interactor.attachTransform.localRotation = interactorRotation;
    }

    private void ClearInteractor(XRBaseInteractor interactor)
    {
        interactorPosition = Vector3.zero;
        interactorRotation = Quaternion.identity;
    }

    

    
   
}
