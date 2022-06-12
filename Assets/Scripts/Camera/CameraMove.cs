using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float scaleSpeed = 1000f;
    public float moveSpeed = 250f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouse = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(new Vector3(h, -mouse * scaleSpeed, v) * moveSpeed * Time.deltaTime, Space.World);
        Vector3 pos = transform.position;
        if (pos.x < -50) pos.x = -50;
        else if (pos.x > 50) pos.x = 50;
        if (pos.y < -4) pos.y = -4;
        else if(pos.y > 12) pos.y = 12;
        if (pos.z < -50) pos.z = -50;
        else if (pos.z > 50) pos.z = 50;
        transform.position = pos;
    }
}
