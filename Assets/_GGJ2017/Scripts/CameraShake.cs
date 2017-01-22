using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{

    Vector3 originalCameraPosition;

    float shakeAmt = 5;

    public Camera mainCamera;

    void StartShake()
    {        
        InvokeRepeating("Shake", 0, .01f);
        Invoke("StopShaking", 0.1f);
    }

    void Shake()
    {
        if (shakeAmt > 0)
        {
            float quakeAmt = Random.value * shakeAmt * 2 - shakeAmt;
            Vector3 pp = mainCamera.transform.position;
            pp.y += quakeAmt; // can also add to x and/or z
            mainCamera.transform.position = pp;
        }
    }

    void StopShaking()
    {
        CancelInvoke("Shake");
        mainCamera.transform.position = originalCameraPosition;
    }

}