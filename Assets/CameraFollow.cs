using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float distance;
    public float zOff;
    public float yOffSpeed = 10;
    public float zOffSpeed = 50;
    public float xRotSpeed = 50;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(target.position.x, target.position.y + distance, target.position.z - zOff);
       // transform.rotation = Quaternion.Euler(75 - transform.position.z / xRotSpeed, transform.rotation.y, transform.rotation.z);
    }
}
