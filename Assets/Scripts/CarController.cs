using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public bool Started; // Is the car started?
    bool stopped = true;
    public bool GasPedal;
    public bool BrakePedal;
    public float Steering; // 1.0 = hard right, -1.0 = hard left;

    float steeringSpeed = 3f;
    float returnSpeed = 3f;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GasPedal = Input.GetKey(KeyCode.UpArrow);
        BrakePedal = Input.GetKey(KeyCode.DownArrow);
        var right = Input.GetKey(KeyCode.RightArrow);
        var left = Input.GetKey(KeyCode.LeftArrow);
        
        if (right && !left) {
            Steering += steeringSpeed * Time.deltaTime;
        } else if (left && !right) {
            Steering -= steeringSpeed * Time.deltaTime;
        } else if (Steering != 0) {
            var correction = returnSpeed * Time.deltaTime;
            if (Steering > 0) correction *= -1;
            var newSteering = Steering + correction;
            if (newSteering * Steering < 0) {
                // Steering flipped!
                newSteering = 0;
            }

            Steering = newSteering;
        }

        if (Steering > 1f) Steering = 1f;
        if (Steering < -1f) Steering = -1f;
        if (Steering > -0.01f && steeringSpeed < 0.01f) Steering = 0;


    }
}
