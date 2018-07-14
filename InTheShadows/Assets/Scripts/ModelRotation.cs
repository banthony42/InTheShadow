﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRotation : MonoBehaviour
{

    public bool rotation_y;
    public bool rotation_x;
    public bool move;
    public float moveSpeed;
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
        limit = 0.5f;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        positionDest = transform.position;
        rotationDest = transform.rotation;
    }

    bool checkWin()
    {
        if (currentLevel == 0)
        {
            float delta = Mathf.DeltaAngle(transform.rotation.eulerAngles.y, 180);
            if (delta >= (-1 * limit) && delta <= limit)
                return true;
        }
        if (currentLevel == 1)
        {
        }
        return false;
    }

    // Dont work
    //bool deltaQuaternion(Quaternion delta, float lim)
    //{
    //    //if (delta.x < (-1 * lim) && delta.x > lim)
    //        //return false;
    //    if (delta.y >= (-1 * lim) && delta.y <= lim)
    //        return false;
    //    //else if (delta.z < (-1 * lim) && delta.z > lim)
    //    //return false;
    //    return true;
    //}

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
                if (rotation_x && Input.GetKey(KeyCode.LeftControl))
                    transform.Rotate(Vector3.right * Input.GetAxis("Mouse Y") * Time.deltaTime * 100);
                else if (rotation_y)
                {
                    //rotationDest = transform.rotation;
                    //rotationDest.eulerAngles += new Vector3(0, Input.GetAxis("Mouse X") * 10, 0);
                    transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * 100);
                }
            }
        }

        if (move && Vector3.Distance(transform.position, positionDest) > 0.2f)
            transform.position = Vector3.Lerp(transform.position, positionDest, Time.deltaTime * moveSpeed);

        // Dont work
        //if (deltaQuaternion(rotationDest * Quaternion.Inverse(transform.rotation), limit))
            //transform.rotation = Quaternion.Lerp(transform.rotation, rotationDest, Time.deltaTime * moveSpeed);
    }
}
