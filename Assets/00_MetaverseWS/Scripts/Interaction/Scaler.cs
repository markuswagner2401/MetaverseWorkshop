using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Normal.Realtime;

public class Scaler : MonoBehaviour
{
    [SerializeField]InputActionReference scaleInputRef;
    [SerializeField] float scaleFactor = 0.01f;
    [SerializeField] float scale = 1f;

    [SerializeField] ScaleSync scaleSync;

    Vector3 scaleVector = new Vector3(1f,1f,1f);

    bool scaleActive = false;

    private void Awake() 
    {
        if(scaleSync != null)
        {
            scaleSync = GetComponent<ScaleSync>();
        }
        
    }

    private void Start() 
    {
        //scale = transform.localScale.x;
    }

    private void Update() 
    {
        if(scaleActive && Mathf.Abs(scaleInputRef.action.ReadValue<Vector2>().y) > 0f )
        {
            ScalePerFrame(scaleInputRef.action.ReadValue<Vector2>().y);
        }

        

        // transform.localScale = scaleVector * scale;
        
    }


    
    private void ScalePerFrame(float amount)
    {
        print("scale");
        scale += amount * scaleFactor;
        scaleSync.ChangeLocalScale(scale);
       
    }

    public void ActivateScale(bool value)
    {
        GetCurrentScale();
        scaleActive = value;
    }

    private void GetCurrentScale()
    {
        scale = scaleSync.GetCurrentModelScale();
    }
  
  
}
