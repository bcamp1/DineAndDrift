using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicBehavior : MonoBehaviour
{
    public Vector3 velocity; // Units per second
    public Vector3 acceleration; // Units per second^2

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
        velocity += acceleration * Time.deltaTime;
    }
}
