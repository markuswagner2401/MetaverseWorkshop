using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using System;
using UnityEngine.Video;
using UnityEngine.Rendering;


public class MaterialSync : RealtimeComponent<MaterialSyncModel>
{
    
    [SerializeField] MaterialState[] materialStates;

    


    [System.Serializable]
    private struct MaterialState
    {
        public string note;
        public Material material;

        public Texture texture;
        public VideoClip videoClip;

        //public bool is360Video;

        

        public float uvScaleX;

        public float uvScaleY;

        public float uvOffsetX;

        public float uvOffsetY;

        

        

        [HideInInspector]
        public RenderTexture videoRT;

        //public long startFrame;

        [HideInInspector]
        public bool videoHasStarted;

        //public bool playVideoOnActivate;

        [HideInInspector]
        public bool play;

        [HideInInspector]
        public long currentFrame;
    }

    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] VideoPlayer videoPlayer;

  

    [SerializeField] string hoverOverlayStrengthRef;

    [SerializeField] string textureRef;

    [SerializeField] string uvXScaleRef;
    [SerializeField] string uvYScaleRef;

    [SerializeField] string uvOffsetXRef;

    [SerializeField] string uvOffsetYRef;


    [SerializeField] bool replaceStartStateWithCurrentMaterial;

    [SerializeField] bool playActiveAtStart;

    // [SerializeField] RenderTexture hDVideoRT;
    // [SerializeField] RenderTexture video360RT;

   

    private MaterialPropertyBlock materialPropertyBlock;

    

   
    

    

    int stateIndexAtStart = 0;
    
    

    int currentMaterialIndex;
    int lastMaterialIndex;
    
    float currentHoverOverlayStrength = 0f;



    
    
    

    
    

    private void Awake() 
    {
        if (stateIndexAtStart >= materialStates.Length)
        {
            currentMaterialIndex = 0;
        }

        else
        {
            currentMaterialIndex = stateIndexAtStart;
        }

        

        if (meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        if(replaceStartStateWithCurrentMaterial)
        {
            materialStates[currentMaterialIndex].material = meshRenderer.material;
            materialStates[currentMaterialIndex].texture = meshRenderer.material.GetTexture(textureRef);
            
        }

        if(videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        materialPropertyBlock = new MaterialPropertyBlock();

        


    }

    void Start()
    {
        
        for (int i = 0; i < materialStates.Length; i++)
        {
            if(materialStates[i].videoClip == null) continue;

            int width = (int)materialStates[i].videoClip.width;
            int height = (int)materialStates[i].videoClip.height;

            materialStates[i].videoRT = new RenderTexture(width, height, 16, RenderTextureFormat.Default);
            materialStates[i].videoRT.name = gameObject.name + materialStates[i].note;

            // if(materialStates[i].is360Video)
            // {
            //     materialStates[i].videoRT = video360RT;
            // }

            // else
            // {
            //     materialStates[i].videoRT = hDVideoRT;
            // }

            

            
        }

        if(playActiveAtStart)
        {
            SetNextActiveMaterialState();
        }
        
    }

    
    void Update()
    {
        meshRenderer.material.SetFloat(uvXScaleRef, materialStates[model.materialIndex].uvScaleX);
        meshRenderer.material.SetFloat(uvYScaleRef, materialStates[model.materialIndex].uvScaleY);

        meshRenderer.material.SetFloat(uvOffsetXRef, materialStates[model.materialIndex].uvOffsetX);
        meshRenderer.material.SetFloat(uvOffsetYRef, materialStates[model.materialIndex].uvOffsetY);
    }

    public void SetNextActiveMaterialState()
    {
        StartCoroutine(SetNextActiveMaterialRoutine());
        

    }

    IEnumerator SetNextActiveMaterialRoutine()
    {
        if(materialStates.Length <= 1) yield break;
       
        lastMaterialIndex = currentMaterialIndex;

        yield return CaptureVideoFrame(lastMaterialIndex);
        
        currentMaterialIndex += 1;
        if(currentMaterialIndex >= materialStates.Length)
        {
            currentMaterialIndex = 1;
        }

        SetModel(currentMaterialIndex);

        yield break;
    }

    IEnumerator CaptureVideoFrame(int index)
    {
        model.playVideo = false;

        if(materialStates[index].videoClip == null) yield break;

        materialStates[index].currentFrame = videoPlayer.frame;

        yield return null;
    }

    private void SetModel(int index)
    {
        model.materialIndex = index;

        if(materialStates[model.materialIndex].videoClip != null)
        {
            model.playVideo = true;

            // if(materialStates[model.materialIndex].videoHasStarted)
            // {
            //     model.videoFrame = materialStates[model.materialIndex].currentFrame;
            // }

            // else
            // {
            //     model.videoFrame = materialStates[model.materialIndex].startFrame;
            // }

            model.videoFrame = (int)materialStates[model.materialIndex].currentFrame;

            // print("current frame " + materialStates[index].note + " " + model.videoFrame);
        }

        else
        {
            model.playVideo = false;
        }
    }

    public void SetInactiveMaterialState()
    {
        currentMaterialIndex = 0;
        model.materialIndex = currentMaterialIndex;
    }

    

    public void SetHoverOverlayStrength(float strength)
    {
        currentHoverOverlayStrength = strength;

        model.hoverOverlayStrength = currentHoverOverlayStrength;
    }





    protected override void OnRealtimeModelReplaced(MaterialSyncModel previousModel, MaterialSyncModel currentModel)
    {
        //base.OnRealtimeModelReplaced(previousModel, currentModel);

        if(previousModel != null)
        {
            previousModel.materialIndexDidChange -= MaterialIndexDidChange;
            
            previousModel.hoverOverlayStrengthDidChange -= HoverOverlayStrengthDidChange;
            previousModel.playVideoDidChange -= PlayVideoDidChange;
            previousModel.videoFrameDidChange -= VideoFrameDidChange;
        }

        if(currentModel != null)
        {
            if (currentModel.isFreshModel)
            {  
                model.materialIndex = currentMaterialIndex;
                model.hoverOverlayStrength = currentHoverOverlayStrength;
            }

            PlayMaterialState();
            ChangeHoverOverlayStrength();
            PlayVideo();
            ChangeVideoFrame();

            currentModel.materialIndexDidChange += MaterialIndexDidChange;
           
            currentModel.hoverOverlayStrengthDidChange += HoverOverlayStrengthDidChange;
            currentModel.playVideoDidChange += PlayVideoDidChange;
            currentModel.videoFrameDidChange += VideoFrameDidChange;
        }
    }

    

    private void MaterialIndexDidChange(MaterialSyncModel model, int value)
    {
        PlayMaterialState();
    }

  


    private void HoverOverlayStrengthDidChange(MaterialSyncModel model, float value)
    {
        ChangeHoverOverlayStrength();
    }

    private void PlayVideoDidChange(MaterialSyncModel model, bool value)
    {
       
        
        PlayVideo();
    }

    

    private void VideoFrameDidChange(MaterialSyncModel model, float value)
    {
        ChangeVideoFrame();
    }

    

    private void PlayMaterialState()
    {
        if(meshRenderer == null) return;

        

        //material

        if(materialStates[model.materialIndex].material != null && materialStates[model.materialIndex].material != meshRenderer.material)
        {
            meshRenderer.material = materialStates[model.materialIndex].material;
        }

        if(materialStates[model.materialIndex].texture != null)
        {
            //meshRenderer.GetPropertyBlock(materialPropertyBlock);
            materialPropertyBlock.SetTexture(textureRef, materialStates[model.materialIndex].texture);
            meshRenderer.SetPropertyBlock(materialPropertyBlock);
        }

        meshRenderer.material.SetFloat(uvXScaleRef, materialStates[model.materialIndex].uvScaleX);
        meshRenderer.material.SetFloat(uvYScaleRef, materialStates[model.materialIndex].uvScaleY);



        // video

        ChangeVideoFrame();

        PlayVideo();

         
    }

    

    private void ChangeHoverOverlayStrength()
    {
        if(meshRenderer == null) return;
        meshRenderer.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetFloat(hoverOverlayStrengthRef, model.hoverOverlayStrength);
        meshRenderer.SetPropertyBlock(materialPropertyBlock);
    }

    private void PlayVideo()
    {
        if(model.playVideo)
        {
            videoPlayer.clip = materialStates[model.materialIndex].videoClip;

            videoPlayer.targetTexture = materialStates[model.materialIndex].videoRT;

            materialPropertyBlock.SetTexture(textureRef, materialStates[model.materialIndex].videoRT);

            meshRenderer.SetPropertyBlock(materialPropertyBlock);

            
            
            videoPlayer.Play();
        }

        else
        {
            videoPlayer.Pause();
        }
    }

    private void ChangeVideoFrame()
    {
        if(model.playVideo)
        {
            videoPlayer.frame = Convert.ToInt64(model.videoFrame);
        }
        
    }
}
