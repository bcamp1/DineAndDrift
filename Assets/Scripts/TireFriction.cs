using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireFriction : MonoBehaviour
{
    public float SlidingCoefficient = 0.5f;
    public float RollingCoefficient = 0.2f;

    public float SupportedWeight = 10f; // N

    public float MaxStaticFriction = 500f;

    public Vector3 ReactionForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool sliding;
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Vector3 vel = transform.InverseTransformDirection(body.velocity);
        
        HingeJoint2D hinge = GetComponent<HingeJoint2D>();
        ReactionForce = transform.InverseTransformDirection(hinge.reactionForce);


        float slidingMagnitude = SlidingCoefficient * SupportedWeight;
        float rollingMagnitude = RollingCoefficient * SupportedWeight;

        if (Mathf.Abs(ReactionForce.x) < MaxStaticFriction) {
            sliding = false;
            slidingMagnitude = Mathf.Abs(ReactionForce.x);
            renderer.color = Color.black;
        } else {
            // Slipping!
            sliding = true;
            renderer.color = Color.red;
        }


        
        float xNorm, yNorm;
        if (vel.x == 0) xNorm = 0;
        else xNorm = vel.x / Mathf.Abs(vel.x);

        if (vel.y == 0) yNorm = 0;
        else yNorm = vel.y / Mathf.Abs(vel.y);
        Debug.Log(vel);

        
        
        Vector3 forceVec = new Vector3(xNorm * slidingMagnitude, -yNorm* rollingMagnitude, 0f);
        body.AddRelativeForce(forceVec);

        // if (!sliding) {
        //     transform.localPosition = hinge.connectedAnchor;
        // }
    }
}
