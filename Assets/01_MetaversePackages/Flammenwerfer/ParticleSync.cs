using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using System;

public class ParticleSync : RealtimeComponent<ParticleSyncModel>
{
    ParticleSystem particleSystem;
    
    void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    
    void Update()
    {
        
    }
    
    public void SetEmissionModel(bool value)
    {
        model.emissionEnabled = value;
    }


    protected override void OnRealtimeModelReplaced(ParticleSyncModel previousModel, ParticleSyncModel currentModel)
    {
        if(previousModel != null)
        {
            previousModel.emissionEnabledDidChange -= EmissionEnabledDidChange;
        }

        if(currentModel != null)
        {
            if(currentModel.isFreshModel)
            {
                model.emissionEnabled = particleSystem.emission.enabled;
            }

            ChangeEmission();


            currentModel.emissionEnabledDidChange += EmissionEnabledDidChange;
        }
    }

    private void EmissionEnabledDidChange(ParticleSyncModel model, bool value)
    {
        ChangeEmission();
    }

    private void ChangeEmission()
    {
        var emission = particleSystem.emission;
        emission.enabled = model.emissionEnabled;
    }
}
