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
    }
}
