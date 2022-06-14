using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using System;


public class ScaleSync : RealtimeComponent<ScaleSyncModel>
{
    [SerializeField] Vector3 capturedScale = new Vector3();
    float scaleFactor;

    private void Awake() 
    {
        capturedScale = transform.localScale;
    }
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void ChangeLocalScale(float value)
    {
        model.scale = value;
    }

    public float GetCurrentModelScale()
    {
        return model.scale;
    }


    protected override void OnRealtimeModelReplaced(ScaleSyncModel previousModel, ScaleSyncModel currentModel)
    {
        //base.OnRealtimeModelReplaced(previousModel, currentModel);

        if(previousModel != null)
        {
            previousModel.scaleDidChange -= ScaleDidChange;
        }

        if(currentModel != null)
        {
            if (currentModel.isFreshModel)
            {
                model.scale = transform.localScale.x;
                model.scale = 1f;
            }

            UpdateLocalScale();

            currentModel.scaleDidChange += ScaleDidChange;
        }
    }

    private void ScaleDidChange(ScaleSyncModel model, float value)
    {
        UpdateLocalScale();
    }

    private void UpdateLocalScale()
    {
        
        transform.localScale = capturedScale * model.scale;
    }


}
