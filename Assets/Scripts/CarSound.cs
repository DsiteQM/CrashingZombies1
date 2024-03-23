using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSound : MonoBehaviour
{

    public float minSpeed;
    public float maxSpeed;
    private float currentSpeed;

    Rigidbody carRB;
    AudioSource carAudio;

    public float minPitch;
    public float maxPitch;
    private float pitchFromCar;

    private void Start()
    {
        carRB= GetComponent<Rigidbody>();
        carAudio= GetComponent<AudioSource>();
    }

    private void Update()
    {
        EngineSound();
    }
    private void EngineSound()
    {
        currentSpeed = carRB.velocity.magnitude;
        pitchFromCar = carRB.velocity.magnitude / 20f;

        if(currentSpeed < minSpeed)
        {
            carAudio.pitch = minPitch;
        }

        if(currentSpeed > minSpeed && currentSpeed <maxSpeed) 
        {
            carAudio.pitch = minPitch + pitchFromCar;
        }

        if(currentSpeed > maxSpeed)
        {
            carAudio.pitch = maxPitch;
        }
    }
}
