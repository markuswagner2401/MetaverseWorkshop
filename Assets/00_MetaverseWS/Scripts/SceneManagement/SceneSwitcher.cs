using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SceneSwitcher : MonoBehaviour
{

    [SerializeField] InputActionReference sceneSwitchInputActionRef;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if(sceneSwitchInputActionRef.action.ReadValue<float>() > 0f)
        {
            print("change Scene button");
            GetComponent<SceneLoader>().ChangeScene();
        }
    }
}
