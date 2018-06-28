using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelectLevelScript : MonoBehaviour
{

    public List<GameObject> level;
    public AudioClip levelSwhitchSound;
    public AudioClip buttonClic;
    public UnityEngine.UI.Text UI_levelName;
    public GameObject fade;

    private AudioSource player;
    private Vector3 cameraDestination;
    private Vector3 startPosition;    // pos(0,-45,15) rot(-45,0,0)
    private int indexLevel = -1;
    private float remaining;
    private float remainingLimit = 5f;
    private float speedTravel = 5f;
    private int tmpIndex;
    private Camera myCam;
    private string sceneToLoad = "none";
    private string saveTextChapter;

    // Use this for initialization
    void Start()
    {
        myCam = gameObject.GetComponent<Camera>();
        player = GetComponent<AudioSource>();
        startPosition = transform.position;
        cameraDestination = Vector3.zero;
        saveTextChapter = UI_levelName.text;
    }

    void launchLevel(RaycastHit hit)
    {
        LevelScript levelChoose = hit.collider.GetComponent<LevelScript>();
        player.clip = buttonClic;
        player.Play();
        fade.GetComponent<UnityEngine.UI.RawImage>().raycastTarget = true;
        fade.GetComponent<FadeScript>().target = Color.black;
        fade.GetComponent<FadeScript>().timer = 0f;
        hit.collider.GetComponent<Animator>().SetTrigger("levelSelect");
        sceneToLoad = "Menu"; //sceneToLoad = hit.collider.GetComponent<LevelScript>().levelName;
    }

    // Update is called once per frame
    void Update()
    {
        // New scene loading
        if (sceneToLoad != "none" && player.time > 1.3f)
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);

        // Dezoom effect on the beginning of the scene
        if (myCam.fieldOfView < 60)
        {
            if (myCam.fieldOfView < 1.1)
                player.PlayOneShot(levelSwhitchSound);
            myCam.fieldOfView += (myCam.fieldOfView * speedTravel) * Time.deltaTime;
        }

        // Back to the menu
        if (Input.GetKeyDown("escape"))
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");

        // Temporaire 
        if (Input.GetKeyDown(KeyCode.U))
        {
            LevelScript tmp = level[indexLevel].GetComponent<LevelScript>();
            UserSave.userP.setState(tmp.levelName, 1);
        }

        // Clic Handler on a level
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.tag == "level")
                {
                    if ((tmpIndex = hit.collider.GetComponent<LevelScript>().levelIndex) != indexLevel)
                    {
                        indexLevel = tmpIndex;
                        cameraDestination = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y - 10f, 40f);
                        player.PlayOneShot(levelSwhitchSound);
                    }
                    else
                        launchLevel(hit);
                }
            }
        }
        // Scroll to increment or decrement level view
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (indexLevel >= 0 && remaining <= remainingLimit)
            {
                indexLevel--;
                player.PlayOneShot(levelSwhitchSound);
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (indexLevel < level.Count - 1 && remaining <= remainingLimit)
            {
                indexLevel++;
                player.PlayOneShot(levelSwhitchSound);
            }
        }

        // Calcul of the new position
        if (indexLevel >= 0)
        {
            UI_levelName.text = level[indexLevel].GetComponent<LevelScript>().levelName;
            cameraDestination = level[indexLevel].transform.position;
            cameraDestination.y -= 10;
            cameraDestination.z = 40f;
        }
        // Si index < 0, la camera se met en position initial
        else
        {
            UI_levelName.text = saveTextChapter;
            cameraDestination = startPosition;
        }

        // Camera movement
        if (cameraDestination != Vector3.zero)
            transform.position = Vector3.Lerp(transform.position, cameraDestination, Time.deltaTime * speedTravel);
        remaining = Vector3.Distance(transform.position, cameraDestination);
    }
}
