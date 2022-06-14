using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using System;

public class ColorSync : RealtimeComponent<ColorSyncModel>
{
    private MeshRenderer _meshRenderer;

    private void Awake() 
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void UpdateMeshRenderColor()
    {
        _meshRenderer.material.color = model.color;
    }

    protected override void OnRealtimeModelReplaced(ColorSyncModel previousModel, ColorSyncModel currentModel)
    {
        if(previousModel != null)
        {
            previousModel.colorDidChange -= ColorDidChange;
        }

        if(currentModel != null)
        {
            if(currentModel.isFreshModel)
            {
                currentModel.color = _meshRenderer.material.color;
            }

            UpdateMeshRenderColor();

            currentModel.colorDidChange += ColorDidChange;
        }


    }

    private void ColorDidChange(ColorSyncModel model, Color value)
    {
        UpdateMeshRenderColor();
    }

    public void SetColor(Color color)
    {
        model.color = color;
    }
}
