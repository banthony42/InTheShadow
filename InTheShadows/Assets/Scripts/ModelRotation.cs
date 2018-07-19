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

    public ModelRotation dependance;

    private Transform target;
    private bool _win;

    public bool win
    {
        get { return _win; }
    }

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
                if (!move || (move && Vector3.Distance(transform.localPosition, winPosition) <= 0.3f))
                {
                    if (!dependance || dependance && dependance.win)
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
            positionDest = transform.position;
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
            if (move)
            {
                // Raycast on mouse pointer and move the object hit
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 1000))
                {
                    Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 1);
                    if (hit.collider.tag == "model")
                    {
                        target = hit.collider.transform;
                        initialPosition = target.position;
                        initialRotation = target.rotation;
                        positionDest = target.position;
                    }
                }
            }

            if (move && Input.GetKey(KeyCode.LeftShift))
                positionDest = new Vector3(target.position.x, target.position.y + Input.GetAxis("Mouse Y") * moveSpeed, target.position.z);
            else
            {
                if (rotation_x && Input.GetKey(KeyCode.LeftControl))
                    target.Rotate(Input.GetAxis("Mouse Y") * Time.deltaTime * 100, 0, 0, Space.Self);
                else if (rotation_y)
                    target.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * 100, 0, Space.World);
            }
        }
        if (move && Vector3.Distance(target.position, positionDest) > 0.2f)
            target.position = Vector3.Lerp(target.position, positionDest, Time.deltaTime * moveSpeed);
    }
}
