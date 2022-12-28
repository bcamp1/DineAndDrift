using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarThings : MonoBehaviour
{
    CarController controls;
    static Vector2[] frontWheelLocs = {new Vector2(0.6f, 0.6f), new Vector2(-0.6f, 0.6f)};
    static Vector2[] rearWheelLocs = {new Vector2(0.6f, -0.8f), new Vector2(-0.6f, -0.8f)};

    static float MaxSteerAngle = 45f; // Degrees
    static float MaxWheelForce = 500f;

    Rigidbody2D body;
    

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        controls = GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        float steerAngle = MaxSteerAngle * controls.Steering;
        float wheelForce = controls.GasPedal ? MaxWheelForce : 0;
        body.inertia = 1000;


        var wheelVector = new Vector2(wheelForce * Mathf.Sin(steerAngle * (Mathf.PI/180f)), wheelForce * Mathf.Cos(steerAngle * (Mathf.PI/180f)));
        var globalWheelVector = transform.TransformDirection(wheelVector);
        body.AddForceAtPosition(frontWheelLocs[0], globalWheelVector);
        body.AddForceAtPosition(frontWheelLocs[1], globalWheelVector);

        Debug.Log(globalWheelVector);
    }
}
