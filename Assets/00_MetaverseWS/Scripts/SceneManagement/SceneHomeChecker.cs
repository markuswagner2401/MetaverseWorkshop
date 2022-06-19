using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHomeChecker : MonoBehaviour
{

    private void OnEnable() 
    {
        FindObjectOfType<SceneHome>().SceneHomeCheck(this.gameObject);
    }
    
}
