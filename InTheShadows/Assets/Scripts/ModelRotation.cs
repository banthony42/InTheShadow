using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRotation : MonoBehaviour
{

    public bool rotation_y;
    public bool rotation_x;
    public bool move;
    public float moveSpeed;

    private UnityEngine.Quaternion rotationDest;
    private Vector3 positionDest;

    private Vector3 translateVec;
    private UnityEngine.Quaternion rotateVec;

    // Use this for initialization
    void Start()
    {
        rotationDest = transform.rotation;
        positionDest = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            if (move && Input.GetKey(KeyCode.LeftShift))
            {
                Debug.Log("Update translateVec");
                positionDest = new Vector3(transform.position.x + Input.GetAxis("Mouse X") * moveSpeed, transform.position.y + Input.GetAxis("Mouse Y") * moveSpeed, transform.position.z);
            }
            else
            {
                if (rotation_x && Input.GetKey(KeyCode.LeftControl))
                {
                    transform.Rotate(Vector3.right * Input.GetAxis("Mouse Y") * Time.deltaTime * 100);
                }
                if (rotation_y && !Input.GetKey(KeyCode.LeftControl))
                {
                    transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * 100);
                }
            }
        }

        if (move && Vector3.Distance(transform.position, positionDest) > 0.2f)
            transform.position = Vector3.Lerp(transform.position, positionDest, Time.deltaTime * moveSpeed);
    }
}
