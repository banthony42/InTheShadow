using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRotation : MonoBehaviour
{

    public bool rotation_y;
    public bool rotation_x;
    public bool move;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            if (move && Input.GetKey(KeyCode.LeftControl))
            {
                transform.Translate(Input.GetAxis("Mouse X") * Time.deltaTime, Input.GetAxis("Mouse Y") * Time.deltaTime, 0);
            }
            else
            {
                if (rotation_x)
                {
                    transform.Rotate(Vector3.right * Input.GetAxis("Mouse Y") * Time.deltaTime * 100);
                }
                if (rotation_y)
                {
                    transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * 100);
                }
            }
        }
    }
}
