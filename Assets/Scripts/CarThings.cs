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
    
    Vector3[] frontWheelGlobalLocs() {
        Vector3[] Vec = {transform.TransformPoint(frontWheelLocs[0]), transform.TransformPoint(frontWheelLocs[1])};
        return Vec;
    }

    Vector3[] rearWheelGlobalLocs() {
        Vector3[] Vec = {transform.TransformPoint(rearWheelLocs[0]), transform.TransformPoint(rearWheelLocs[1])};
        return Vec;
    }

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
        body.inertia = 10000;


        var wheelVector = new Vector2(wheelForce * Mathf.Sin(steerAngle * (Mathf.PI/180f)), wheelForce * Mathf.Cos(steerAngle * (Mathf.PI/180f)));
        var globalWheelVector = transform.TransformDirection(wheelVector);
        body.AddForceAtPosition(globalWheelVector, frontWheelGlobalLocs()[0]);
        body.AddForceAtPosition(globalWheelVector, frontWheelGlobalLocs()[0]);

        var tracker = GetComponentInChildren<PointKinematicsTracker>();

        Debug.Log("local: " + tracker.GetLocalPos() + " global: " + tracker.GetGlobalPos());
    }
}
