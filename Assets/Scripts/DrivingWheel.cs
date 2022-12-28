using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingWheel : MonoBehaviour
{

    public CarController controls;
    public float MaxWheelForce = 500f; // Newtons

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (controls.GasPedal && controls.Started) {
            Vector3 forceVec = new Vector3(0, MaxWheelForce, 0);
            GetComponent<Rigidbody2D>().AddRelativeForce(forceVec);
        }
    }
}
