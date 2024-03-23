using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public static CarController Instance { get; set; }

    HealthSystem healthSystem;
    public enum Axel
    {
        Front,
        Rear
    }


    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public Axel axel;
    }

    public TextMeshProUGUI carSpeedText;


    [Header("Physics")]
    public float maxAccelaration = 30f;
    public float brakeAccelaration = 40f;
    public float turnSensivity = 1f;
    public float maxSteerAngle = 25f;

    public List<Wheel> wheels;

    float moveInput;
    float steerInput;
    Rigidbody carRB;
    public AudioSource crashAudio;

    Vector3 _centerOfMass;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        healthSystem = FindObjectOfType<HealthSystem>();
        carRB= GetComponent<Rigidbody>();
        carRB.centerOfMass = _centerOfMass;
    }

    private void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
        Move();
        Steer();
        Brake();
    }

    private void Move()
    {
        foreach(var wheel in wheels)
        {
            if(carRB.velocity.magnitude > 20)
            {
                wheel.wheelCollider.motorTorque = moveInput * maxAccelaration * 750 * Time.deltaTime;
            }
            else
            {
                wheel.wheelCollider.motorTorque = moveInput * maxAccelaration * 1500 * Time.deltaTime;
            }
            
        }
    }

    private void Steer()
    {
        foreach(var wheel in wheels)
        {
            if(wheel.axel == Axel.Front)
            {
                var _steerAngle = steerInput * turnSensivity* maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }

    private void Brake()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque =  brakeAccelaration * 850 * Time.deltaTime;
            }
        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0f;
            }
        }
    
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Wall" && GetSpeed() > 10f)
        {
            int damageAmount = Mathf.RoundToInt(GetSpeed() / 3);
            crashAudio.Play();
            healthSystem.TakeDamage(damageAmount);
        }
    }

    public float GetSpeed()
    {
        return carRB.velocity.magnitude;
    }

}
