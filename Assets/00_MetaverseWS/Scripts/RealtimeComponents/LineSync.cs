using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using System;

public class LineSync : RealtimeComponent<LineSyncModel>
{
    LineRenderer _lineRenderer;

    private void Awake() 
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    public void SetLinePoints(Vector3 point0, Vector3 point1)
    {
        model.point0 = point0;
        model.point1 = point1;
    }

    public void SetEnabled(bool value)
    {
        model.enabled = value;
    }

    public void SetColors(Color color1, Color color2, float alpha1, float alpha2)
    {
        model.color1 = color1;
        model.color2 = color2;
        model.alpha1 = alpha1;
        model.alpha2 = alpha2;

    }

    protected override void OnRealtimeModelReplaced(LineSyncModel previousModel, LineSyncModel currentModel)
    {
        //base.OnRealtimeModelReplaced(previousModel, currentModel);

        if(previousModel != null)
        {
            previousModel.point0DidChange -= Point0DidChange;
            previousModel.point1DidChange -= Point1DidChange;
            previousModel.enabledDidChange -= EnabledDidChange;
            previousModel.color1DidChange -= Color1DidChange;
            
        }

        if(currentModel != null)
        {
            if(currentModel.isFreshModel)
            {
                currentModel.point0 = _lineRenderer.GetPosition(0);
                currentModel.point1 = _lineRenderer.GetPosition(1);
                currentModel.enabled = _lineRenderer.enabled;
                currentModel.color1 = _lineRenderer.colorGradient.colorKeys[0].color;
                currentModel.color2 = _lineRenderer.colorGradient.colorKeys[1].color;
                currentModel.alpha1 = _lineRenderer.colorGradient.alphaKeys[0].alpha;
                currentModel.alpha2 = _lineRenderer.colorGradient.alphaKeys[1].alpha;
            }

            UpdateLinePositions();
            UpdateEnabled();
            UpdateColor();


            currentModel.enabledDidChange += EnabledDidChange;
            currentModel.point0DidChange += Point0DidChange;
            currentModel.point1DidChange += Point1DidChange;
            currentModel.color1DidChange += Color1DidChange;

        }
    }

    








    /////

    private void EnabledDidChange(LineSyncModel model, bool value)
    {
        UpdateEnabled();
    }

    private void Point0DidChange(LineSyncModel model, Vector3 value)
    {
        UpdateLinePositions();
    }

    private void Point1DidChange(LineSyncModel model, Vector3 value)
    {
        UpdateLinePositions();
    }

    

    private void Color1DidChange(LineSyncModel model, Color value)
    {
        UpdateColor();
    }

     

/////

    private void UpdateLinePositions()
    {
        Vector3[] positions = new Vector3[2];
        positions[0] = model.point0;
        positions[1] = model.point1;

        _lineRenderer.SetPositions(positions);
    }

    private void UpdateEnabled()
    {
        _lineRenderer.enabled = model.enabled;
    }

    private void UpdateColor()
    {
        _lineRenderer.colorGradient.colorKeys[0].color = model.color1;
        _lineRenderer.colorGradient.colorKeys[1].color = model.color2;
        _lineRenderer.colorGradient.alphaKeys[0].alpha = model.alpha1;
        _lineRenderer.colorGradient.alphaKeys[0].alpha = model.alpha2;

    }

    


}
