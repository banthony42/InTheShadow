using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelectLevelScript : MonoBehaviour
{

    public List<GameObject> level;
    public AudioClip levelSwhitchSound;

    private AudioSource player;
    private Vector3 cameraDestination;
    private Transform startPosition;    // pos(0,-45,15) rot(-45,0,0)
    private int indexLevel = -1;
    private float remaining;
    private float remainingLimit = 5f;
    private float speedTravel = 5f;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<AudioSource>();
        startPosition = transform;
        cameraDestination = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000))
            {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 1);
                if (hit.collider.tag == "level")
                {
                    cameraDestination = hit.collider.transform.position;
                    cameraDestination.y = hit.collider.transform.position.y - 10;
                    cameraDestination.z = 40f;
                }
            }

        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f )
        {
            Debug.Log("back");
            if (indexLevel >= 0 && remaining <= remainingLimit)
            {
                indexLevel--;
                player.PlayOneShot(levelSwhitchSound);
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            Debug.Log("front");
            if (indexLevel < level.Count - 1 && remaining <= remainingLimit)
            {
                indexLevel++;
                player.PlayOneShot(levelSwhitchSound);
            }
        }

        if (indexLevel >= 0)
        {
            cameraDestination = level[indexLevel].transform.position;
            cameraDestination.y -= 10;
            cameraDestination.z = 40f;
        }
        else
            cameraDestination = startPosition.position;

        if (cameraDestination != Vector3.zero)
            transform.position = Vector3.Lerp(transform.position, cameraDestination, Time.deltaTime * speedTravel);
        remaining = Vector3.Distance(transform.position, cameraDestination);
    }
}
