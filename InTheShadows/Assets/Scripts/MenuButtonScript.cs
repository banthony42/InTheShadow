using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonScript : MonoBehaviour
{

    public FadeScript fade;
    public AudioClip buttonClic;
    public AudioClip buttonTransition;

    private string sceneToLoad = "none";
    private AudioSource soundPlayer;
    private UnityEngine.UI.Text tmp;
    // Use this for initialization
    void Start()
    {
        tmp = GetComponentInChildren<UnityEngine.UI.Text>();
        soundPlayer = GetComponent<AudioSource>();
        soundPlayer.volume = 0.1f;
    }

    public void pointerEnter()
    {
        soundPlayer.PlayOneShot(buttonTransition);
    }

    public void onClickButton(string buttonInfo)
    {
        soundPlayer.volume = 0.5f;
        soundPlayer.clip = buttonClic;
        soundPlayer.Play();
        fade.fadeOut();
        if (buttonInfo == "classic" || buttonInfo == "test")
        {
            UserSave.userP.setDebug(0);
            if (buttonInfo == "test")
                UserSave.userP.setDebug(1);
            sceneToLoad = "LevelSelect";
        }
        else if (buttonInfo == "quitLevel")
            sceneToLoad = "LevelSelect";
        else
            sceneToLoad = buttonInfo;
    }

    public void muteSound()
    {
        if (UserSave.userP.getMute() == false)
            UserSave.userP.setMute(1);
        else if (UserSave.userP.getMute())
            UserSave.userP.setMute(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (UserSave.userP.getMute() && this.name == "SoundButton")
        {
                tmp.color = new Color(0.5f, 0f, 0f, 1f);
                AudioListener.volume = 0f;
        }
        else if (this.name == "SoundButton")
        {
                tmp.color = new Color(1f, 1f, 1f, 1f);
                AudioListener.volume = 1f;
        }

        if (sceneToLoad != "none" && soundPlayer.time > 1.3f)
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
    }
}
