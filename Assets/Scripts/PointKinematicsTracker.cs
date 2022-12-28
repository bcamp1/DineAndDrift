using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointKinematicsTracker : MonoBehaviour
{
    Vector3 currentPos, pastPos, currentVel, pastVel, acc;

    // Start is called before the first frame update
    void Start()
    {
        currentPos = transform.position;
        pastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        pastPos = currentPos;
        pastVel = currentVel;
        currentPos = transform.position;
        var displacement = currentPos - pastPos;
        currentVel = displacement / (Time.deltaTime);
        acc = (currentVel - pastVel) / (Time.deltaTime);
    }

    public Vector3 GetLocalPos() {
        return transform.localPosition;
    }

    public Vector3 GetGlobalPos() {
        return transform.position;
    }

    public Vector3 GetLocalVel() {
        return transform.position;
    }

    public Vector3 GetGlobalVel() {
        return currentVel;
    }

    public Vector3 GetLocalAcc() {
        return transform.position;
    }

    public Vector3 GetGlobalAcc() {
        return acc;
    }
}
