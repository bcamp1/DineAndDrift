using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarThings : MonoBehaviour
{
    CarController controls;

    public GameObject TRWheel;
    public GameObject TLWheel;
    public GameObject BRWheel;
    public GameObject BLWheel;

    PointKinematicsTracker TRTracker, TLTracker, BRTracker, BLTracker;

    static float MaxSteerAngle = 45f; // Degrees
    static float MaxWheelForce = 500f;

    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        controls = GetComponent<CarController>();

        TRTracker = TRWheel.GetComponent<PointKinematicsTracker>();
        TLTracker = TLWheel.GetComponent<PointKinematicsTracker>();
        BRTracker = BRWheel.GetComponent<PointKinematicsTracker>();
        BLTracker = BLWheel.GetComponent<PointKinematicsTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        float steerAngle = MaxSteerAngle * controls.Steering;
        float wheelForce = controls.GasPedal ? MaxWheelForce : 0;
        body.inertia = 10000;
        TRWheel.transform.localEulerAngles = new Vector3(0, 0, -steerAngle);
        TLWheel.transform.localEulerAngles = new Vector3(0, 0, -steerAngle);


        var wheelVector = new Vector2(wheelForce * Mathf.Sin(steerAngle * (Mathf.PI/180f)), wheelForce * Mathf.Cos(steerAngle * (Mathf.PI/180f)));
        var globalWheelVector = transform.TransformDirection(wheelVector);
        body.AddForceAtPosition(globalWheelVector, TRTracker.GetGlobalPos());
        body.AddForceAtPosition(globalWheelVector, TLTracker.GetGlobalPos());
    }
}
