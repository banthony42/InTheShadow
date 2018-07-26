using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public FadeScript fade;
    public AudioClip buttonClic;
    public AudioClip winSound;
    public ModelRotation model1;
    public ModelRotation model2;

    public Animator victoryPanel;
    public Animator cameraAnim;

    private AudioSource player;
    private string sceneToLoad = "none";
    private bool winSoundTrig;
    private bool firstWin;

	// Use this for initialization
	void Start () {
        winSoundTrig = false;
        firstWin = false;
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

        if ((model1 && model1.win) || (model2 && model2.win))
        {
            if (firstWin == false)
            {
                player.PlayOneShot(winSound, 0.1f);
                firstWin = true;
            }
        }
        else if ((model1 && !model1.win) || (model2 && !model2.win))
            firstWin = false;
        if (model1.win && winSoundTrig == false)
        {
            if (!model2 || model2 && model2.win)
            {
                if (UserSave.userP.getDebug() == false)
                {
                    Debug.Log("debug win");
                    UserSave.userP.setState(model1.currentLevel, 1);
                    if (model2)
                        UserSave.userP.setState(model2.currentLevel, 1);
                }
                cameraAnim.SetTrigger("cameraShift");
                victoryPanel.SetTrigger("SlideIn");
                winSoundTrig = true;
                player.clip = winSound;
                player.Play();
            }
        }
	}
}
