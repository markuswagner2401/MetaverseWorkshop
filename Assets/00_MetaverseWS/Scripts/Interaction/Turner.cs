using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Turner : MonoBehaviour
{
    [SerializeField] Vector3 rotationAngles;
    [SerializeField] InputActionReference inputActionRef;

    [SerializeField] float rotationFactor = 0.01f;
    bool rotateEnabled;
    
    void Start()
    {
        rotationAngles = transform.eulerAngles;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if(rotateEnabled && Mathf.Abs(inputActionRef.action.ReadValue<Vector2>().x) > 0)
        {
            rotationAngles = new Vector3(rotationAngles.x, rotationAngles.y + inputActionRef.action.ReadValue<Vector2>().x * rotationFactor, rotationAngles.z);
            transform.eulerAngles = rotationAngles;
        }

        
    }

    public void EnableRotate(bool value)
    {
        rotationAngles = transform.eulerAngles;
        rotateEnabled = value;
    }


}
