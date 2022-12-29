using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ackermann : MonoBehaviour
{
    public GameObject marker;

    public List<GameObject> SteeringWheels;
    CarController controller;
    public float MaxTurnAngle = 30f; // Degrees

    public float l = 1.4f; // Units
    float phi = 0f; // Degrees
    float theta = 0f; // Degrees
    float r = 0f; // Units

    public float axleOffset = 0.8f;

    float speed = 0f;
    float MaxSpeed = 20f; // Units per second

    float acceleration = 4f;
    float stoppingPower = 12f;

    float rollingSlow = 6f;

    float reverseAcceleration = 2f;

    bool isReverse = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isReverse) {
            if (controller.GasPedal && controller.Started) {
                speed += acceleration * Time.deltaTime;
            } else if (controller.BrakePedal && controller.Started) {
                if (speed == 0) {
                    isReverse = true;
                }
                speed -= stoppingPower * Time.deltaTime;
            } else {
                speed -= rollingSlow * Time.deltaTime;
            }

            if (speed < 0) {
                speed = 0;
            } else if (speed > MaxSpeed) {
                speed = MaxSpeed;
            }
        } else {
            if (controller.GasPedal && controller.Started) {
                speed += stoppingPower * Time.deltaTime;
                if (speed >= 0) {
                    isReverse = false;
                }
            } else if (controller.BrakePedal && controller.Started) {
                speed -= reverseAcceleration * Time.deltaTime;
            } else {
                speed += rollingSlow * Time.deltaTime;
            }

            if (speed > 0) {
                speed = 0;
            } else if (speed < -MaxSpeed) {
                speed = -MaxSpeed;
            }
        }

        // Calculate adjusted max turn angle
        var speedPercent = Mathf.Abs(speed) / MaxSpeed;
        var minAnglePercent = 0.1f; // Angle at max speed
        var anglePercent = (1 - speedPercent) * (1 - minAnglePercent) + minAnglePercent;
        var adjustedMaxTurnAngle = MaxTurnAngle * anglePercent;
        phi = adjustedMaxTurnAngle * controller.Steering;

        if (phi == 0) phi = 0.1f;

        theta = simplifyDeg(transform.eulerAngles[2] + 90f);
        r = phi == 0 ? 0 : l / (Mathf.Tan(toRads(phi)));

        var axleAngle = 90f - theta;
        Vector3 offset = new Vector3(-axleOffset * cos(theta), -axleOffset * sin(theta), 0);

        Vector3 IC = new Vector3(r * cos(axleAngle), -r * sin(axleAngle), 0) + offset + transform.position;

        marker.transform.position = IC;

        // Make Translations/Rotations
        var omega = speed / r;
        transform.RotateAround(IC,  Vector3.forward, -omega * Time.deltaTime * 100f);

        // Animate Steering Wheels
        foreach (GameObject wheel in SteeringWheels) {
            wheel.transform.localEulerAngles = new Vector3(0, 0, -phi);   
        }
    }

    static float toRads(float degrees) {
        return degrees * (Mathf.PI / 180f);
    }

    static float sin(float deg) {
        return Mathf.Sin(toRads(deg));
    }

    static float cos(float deg) {
        return Mathf.Cos(toRads(deg));
    }

    static float tan(float deg) {
        return Mathf.Tan(toRads(deg));
    }

    static float simplifyDeg(float degrees) {
        return degrees % 360;
    }
}
