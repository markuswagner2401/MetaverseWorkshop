using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MaterialChanger : MonoBehaviour
{
    [SerializeField] MaterialState[] materialStates;


    [System.Serializable]
    private struct MaterialState
    {
        public Material material;
        public VideoClip videoClip;

        public bool playVideoOnActivate;
    }

    int inactiveMaterialIndex = 0;
    int currentMaterialIndex = 0;
   
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void SetNextActiveMaterialState()
    {
        currentMaterialIndex += 1;
        if(currentMaterialIndex >= materialStates.Length)
        {
            currentMaterialIndex = 1;
        }

        

    }

    public int GetMaterialsLength()
    {
        return materialStates.Length;
    }

    public void PlayMaterialState(int state)
    {

    }


}
