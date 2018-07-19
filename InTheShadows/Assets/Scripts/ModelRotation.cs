using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRotation : MonoBehaviour
{

    public bool rotation_y;
    public bool rotation_x;
    public bool move;

    public float moveSpeed;
    public float angleIncrement;

    public Vector3 winRotation;
    public Vector3 winPosition;

    public Animator victoryPanel;
    public Animator cameraAnim;
    public int currentLevel;

    private bool _win;

    public bool win
    {
        get { return _win; }
    }

    private Quaternion rotationDest;
    private Vector3 positionDest;
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
        positionDest = transform.position;
        rotationDest = transform.rotation;
    }

    bool checkWin()
    {
        float delta = Mathf.DeltaAngle(transform.rotation.eulerAngles.y, winRotation.y);
        Debug.Log("first " + delta);
        if (delta >= (-1 * limit) && delta <= limit)
        {
            delta = Mathf.DeltaAngle(transform.rotation.eulerAngles.x, winRotation.x);
            Debug.Log("second " + delta);
            if (delta >= (-1 * limit) && delta <= limit)
            {
                Debug.Log("third");
                if (!move || (move && Vector3.Distance(transform.position, winPosition) <= limit))
                    return true;
                else
                    Debug.Log("Third fail");
            }
        }
        return false;
    }

    bool deltaQuaternion(Quaternion delta, float lim)
    {
        float diff = delta.eulerAngles.y - transform.rotation.eulerAngles.y;
        if (diff >= (-1 * lim) && diff <= lim)
        {
            diff = delta.eulerAngles.x - transform.rotation.eulerAngles.x;
            if (diff >= (-1 * lim) && diff <= lim)
                return false;
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            positionDest = transform.position;
            rotationDest = transform.rotation;
        }
        if (checkWin() && !_win)
        {
            cameraAnim.SetTrigger("cameraShift");
            victoryPanel.SetTrigger("SlideIn");
            UserSave.userP.setState(currentLevel, 1);
            _win = true;
        }
        else if (!_win && Input.GetMouseButton(0))
        {
            if (move && Input.GetKey(KeyCode.LeftShift))
                positionDest = new Vector3(transform.position.x + Input.GetAxis("Mouse X") * moveSpeed, transform.position.y + Input.GetAxis("Mouse Y") * moveSpeed, transform.position.z);
            else
            {
                rotationDest = transform.rotation;
                if (rotation_x && Input.GetKey(KeyCode.LeftControl))
                    transform.Rotate(Input.GetAxis("Mouse Y") * Time.deltaTime * 100, 0, 0, Space.Self);
                else if (rotation_y)
                    transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * 100, 0, Space.World);
            }
        }
        if (move && Vector3.Distance(transform.position, positionDest) > limit)
            transform.position = Vector3.Lerp(transform.position, positionDest, Time.deltaTime * moveSpeed);
    }
}
