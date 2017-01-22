using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(AudioSource))]
public class playermanagerB : MonoBehaviour
{
    public float soundAmp;
    public Color colFail = Color.red;
    public Color colWin = Color.green;
    public Color colNormal = Color.white;

    public Light testLight;

    private AudioSource audioSource;

    public float bpm;
    private float time;

    private bool pressed;

    void Start()
    {
        pressed = false;
        testLight.color = colNormal;
        time = 60 / bpm;
        InvokeRepeating("CheckBeat", 0, time);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pressed = true;
        }
    }

    public void CheckBeat()
    {
        Debug.Log("CheckBeat Apertado");

        testLight.gameObject.SetActive(true);
        if (pressed) testLight.color = colWin;
        else testLight.color = colFail;
        pressed = false;

        Invoke("turnOff", 0.1f);
    }

    public void turnOff()
    {
        testLight.gameObject.SetActive(false);
    }

    public void onOnbeatDetected()
    {
        CheckBeat();
    }

    public void onSpectrum(float[] spectrum)
    {
        for (int i = 0; i < spectrum.Length; ++i)
        {
            Vector3 start = new Vector3(i, 0, 0);
            Vector3 end = new Vector3(i, spectrum[i], 0);
            Debug.DrawLine(start, end);
        }
    }
}

