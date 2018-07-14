using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRotation : MonoBehaviour
{

    public bool rotation_y;
    public bool rotation_x;
    public bool move;
    public float moveSpeed;
    public Animator victoryPanel;
    public int currentLevel;
    public string nextLevel;

    private Vector3 positionDest;
    private float limit;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    // Use this for initialization
    void Start()
    {
        limit = 0.5f;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        positionDest = transform.position;
    }

    bool checkWin()
    {
        float delta = Mathf.DeltaAngle(transform.rotation.eulerAngles.y, 180);
        if (delta >=  (-1 * limit) && delta <= limit)
            return true;
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
        if (checkWin())
        {
            Debug.Log("--WIN--");
            victoryPanel.SetTrigger("SlideIn");
            UserSave.userP.setState(currentLevel, 1);
        }
        else if (Input.GetMouseButton(0))
        {
            if (move && Input.GetKey(KeyCode.LeftShift))
                positionDest = new Vector3(transform.position.x + Input.GetAxis("Mouse X") * moveSpeed, transform.position.y + Input.GetAxis("Mouse Y") * moveSpeed, transform.position.z);
            else
            {
                if (rotation_x && Input.GetKey(KeyCode.LeftControl))
                    transform.Rotate(Vector3.right * Input.GetAxis("Mouse Y") * Time.deltaTime * 100);
                else if (rotation_y)
                    transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * 100);
            }
        }

        if (move && Vector3.Distance(transform.position, positionDest) > 0.2f)
            transform.position = Vector3.Lerp(transform.position, positionDest, Time.deltaTime * moveSpeed);
    }
}
