using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ReceiveCastShadow : MonoBehaviour 
{
    public bool castShadows;
    public bool receiveShadows;

    void Start()
    {
        // renderer.receiveShadows = receiveShadows;
        // renderer.castShadows = castShadows;

        GetComponent<Renderer>().shadowCastingMode =  UnityEngine.Rendering.ShadowCastingMode.On;
        GetComponent<Renderer>().receiveShadows = true;
    }

}
