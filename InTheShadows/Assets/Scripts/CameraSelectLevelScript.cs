using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelectLevelScript : MonoBehaviour
{

    public List<GameObject> level;
    public AudioClip levelSwhitchSound;
    public AudioClip buttonClic;
    public UnityEngine.UI.Text UI_levelName;
    public FadeScript fade;

    private AudioSource player;
    private Vector3 cameraDestination;
    private Vector3 startPosition;
    private int indexLevel = -1;
    private float remaining;
    private float remainingLimit = 5f;
    private float speedTravel = 5f;
    private int tmpIndex;
    private Camera myCam;
    private string sceneToLoad = "none";
    private string saveTextChapter;
    private bool _cameraOk;

    public bool cameraOk
    {
        get { return _cameraOk; }
        set { _cameraOk = value; }
    }
    // Use this for initialization
    void Start()
    {
        cameraOk = false;
        myCam = gameObject.GetComponent<Camera>();
        player = GetComponent<AudioSource>();
        startPosition = transform.position;
        cameraDestination = Vector3.zero;
        saveTextChapter = UI_levelName.text;
    }

    void launchLevel(RaycastHit hit)
    {
        LevelScript levelChoose = hit.collider.GetComponent<LevelScript>();
        if (!UserSave.userP.getState(levelChoose.levelIndex - 1) && levelChoose.levelIndex > 0 && !UserSave.userP.getDebug())
            return;

        player.clip = buttonClic;
        player.Play();
        fade.fadeOut();
        hit.collider.GetComponent<Animator>().SetTrigger("levelSelect");
        sceneToLoad = hit.collider.GetComponent<LevelScript>().levelName;
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

        if (myCam.fieldOfView > 59)
            _cameraOk = true;

        // Back to the menu
        if (Input.GetKeyDown("escape"))
        {
            player.clip = buttonClic;
            player.pitch = 1.2f;
            player.Play();
            fade.fadeOut();
            sceneToLoad = "Menu";
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
        // Si index < 0, la camera se met en position initial (vue global du chemin de niveaux)
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
