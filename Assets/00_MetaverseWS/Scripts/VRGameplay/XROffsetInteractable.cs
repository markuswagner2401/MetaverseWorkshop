using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetInteractable : XRGrabInteractable
{
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        MatchAttachPoint(args.interactor);
    }

    

    

    private void MatchAttachPoint(XRBaseInteractor interactor)
    {
        bool isDirect = interactor is XRDirectInteractor || interactor is XRRayInteractor;
        
        attachTransform.position = isDirect? interactor.attachTransform.position : transform.position;
        attachTransform.rotation = isDirect? interactor.attachTransform.rotation : transform.rotation;
    }

    
}
