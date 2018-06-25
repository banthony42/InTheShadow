using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonScript : MonoBehaviour {

    public AudioClip buttonClic;
    public AudioClip buttonTransition;

    private string sceneToLoad = "none";
    private AudioSource soundPlayer;

    // Use this for initialization
	void Start () {
        soundPlayer = GetComponent<AudioSource>();
        soundPlayer.volume = 0.1f;
	}
	
    public void pointerEnter()
    {
        Debug.Log("test");
        soundPlayer.PlayOneShot(buttonTransition);
    }

    public void onClickButton(string button)
    {
        soundPlayer.volume = 0.5f;
        soundPlayer.clip = buttonClic;
        soundPlayer.Play();
        if (button == "classic" || button == "test")
            sceneToLoad = "LevelSelect";
    }

	// Update is called once per frame
	void Update () {
        if (sceneToLoad != "none" && soundPlayer.time > 1.3f)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
            sceneToLoad = "none";
        }
	}
}
