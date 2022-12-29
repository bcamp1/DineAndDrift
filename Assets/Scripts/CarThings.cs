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
    public GameObject TRNode;
    public GameObject TLNode;
    public GameObject BRNode;
    public GameObject BLNode;

    PointKinematicsTracker TRTracker, TLTracker, BRTracker, BLTracker;
    FixedJoint2D TRJoint, TLJoint, BRJoint, BLJoint;

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

        TRJoint = TRNode.GetComponent<FixedJoint2D>();
        TLJoint = TLNode.GetComponent<FixedJoint2D>();
        BRJoint = BRNode.GetComponent<FixedJoint2D>();
        BLJoint = BLNode.GetComponent<FixedJoint2D>();
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

        Debug.Log("bl: " + BLJoint.reactionForce + " br: " + BRJoint.reactionForce);
        BLJoint.GetComponent<Rigidbody2D>().AddForce(new Vector3(-BLJoint.reactionForce.x*0.01f, -BLJoint.reactionForce.y*0.01f, 0));
        BRJoint.GetComponent<Rigidbody2D>().AddForce(new Vector3(-BRJoint.reactionForce.x*0.01f, -BRJoint.reactionForce.y*0.01f, 0));
    }
}
