using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRotation : MonoBehaviour
{

    public bool rotation_y;
    public bool rotation_x;
    public bool move;
    public bool enableFlip;

    public float moveSpeed;

    public Vector3 winRotation;
    public Vector3 winPosition;
    public float posLimit;

    public int currentLevel;

    private Transform target;
    private bool _win;

    public bool win
    {
        get { return _win; }
    }

    private float limit;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    // Use this for initialization
    void Start()
    {
        _win = false;
        limit = 2f;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        target = transform;
    }

    bool checkWin()
    {
        float delta = Mathf.DeltaAngle(transform.rotation.eulerAngles.y, winRotation.y);
        if (delta >= (-1 * limit) && delta <= limit)
        {
            delta = Mathf.DeltaAngle(transform.rotation.eulerAngles.x, winRotation.x);
            if (delta >= (-1 * limit) && delta <= limit)
            {
                if (!move || (move && Vector3.Distance(transform.localPosition, winPosition) <= posLimit))
                    return true;
            }
        }
        if (enableFlip)
        {
            delta = Mathf.DeltaAngle(transform.rotation.eulerAngles.y, winRotation.y + 180);
            if (delta >= (-1 * limit) && delta <= limit)
            {
                delta = Mathf.DeltaAngle(transform.rotation.eulerAngles.x, winRotation.x + 180);
                if (delta >= (-1 * limit) && delta <= limit)
                {
                    if (!move || (move && Vector3.Distance(transform.localPosition, winPosition) <= posLimit))
                        return true;
                }
            }            
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            target = null;
        }
        if (move && !_win && Input.GetMouseButtonDown(0))
        {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 1000))
                {
                    Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 1);
                    if (hit.collider.tag == "model")
                        target = hit.collider.transform;
                }
        }
        _win = checkWin();
        if (!_win && Input.GetMouseButton(0))
        {
            if (move && Input.GetKey(KeyCode.LeftShift))
                target.Translate(Vector3.up * Input.GetAxis("Mouse Y") * Time.deltaTime * moveSpeed, Space.World);
            else
            {
                if (rotation_x && Input.GetKey(KeyCode.LeftControl))
                    target.Rotate(Input.GetAxis("Mouse Y") * Time.deltaTime * 100, 0, 0, Space.Self);
                else if (rotation_y)
                    target.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * 100, 0, Space.World);
            }
        }
    }
}
