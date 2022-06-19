using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHome : MonoBehaviour
{
    [SerializeField] string sceneHomeTag;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void SceneHomeCheck(GameObject gameObject)
    {
        if(gameObject.tag != sceneHomeTag)
        {
            DoIfNotAtHome(gameObject);
        }
    }

    void DoIfNotAtHome(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

}
