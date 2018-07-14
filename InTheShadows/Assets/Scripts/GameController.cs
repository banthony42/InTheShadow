using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public FadeScript fade;
    public AudioClip buttonClic;
    public AudioClip winSound;
    public ModelRotation model;

    private AudioSource player;
    private string sceneToLoad = "none";
    private bool winSoundTrig;

	// Use this for initialization
	void Start () {
        winSoundTrig = false;
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

        if (model.win && winSoundTrig == false)
        {
            winSoundTrig = true;
            player.clip = winSound;
            player.Play();
        }
	}
}
