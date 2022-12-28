using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningWheel : MonoBehaviour
{
    public float MaxTurnAngle = 30; // Degrees
    public float TurnPercent; // 1.0 = hard right, -1.0 = hard left
    // Start is called before the first frame update

    public float Angle;

    public CarController controls;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TurnPercent = controls.Steering;
        Angle = MaxTurnAngle * TurnPercent;
        Vector3 rotateVec = new Vector3(0, 0, -Angle);
        transform.localEulerAngles = rotateVec;
    }
}
