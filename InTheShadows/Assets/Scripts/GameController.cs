using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public FadeScript fade;
    public AudioClip buttonClic;

    private AudioSource player;
    private string sceneToLoad = "none";

	// Use this for initialization
	void Start () {
        player = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        // New scene loading
        if (sceneToLoad != "none" && player.time > 1.3f)
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
        
        if (Input.GetKeyDown("escape"))
        {
            player.clip = buttonClic;
            player.pitch = 1.2f;
            player.Play();
            fade.fadeOut();
            sceneToLoad = "LevelSelect";
        }
	}
}
