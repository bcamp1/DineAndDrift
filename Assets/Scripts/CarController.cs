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

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().inertia = 1000f;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().inertia = 1000f;
        GasPedal = Input.GetKey(KeyCode.UpArrow);
        BrakePedal = Input.GetKey(KeyCode.DownArrow);
        
        if (Input.GetKey(KeyCode.RightArrow)) {
            Steering += 3f * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow)) {
            Steering -= 3f * Time.deltaTime;
        }

        if (Steering > 1f) Steering = 1f;
        if (Steering < -1f) Steering = -1f;


    }
}
