using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMount : MonoBehaviour
{
    public Camera cam;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 objectPos = transform.position;
        cam.transform.position = new Vector3(objectPos.x, objectPos.y, -10f);
    }
}
