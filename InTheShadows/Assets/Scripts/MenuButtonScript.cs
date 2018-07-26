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
    private bool mute;

    // Use this for initialization
    void Start()
    {
        mute = false;
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
        Debug.Log("clic");
        if (!mute)
        {
            AudioListener.volume = 0f;
            UnityEngine.UI.Text tmp = GetComponentInChildren<UnityEngine.UI.Text>();
            if (tmp)
                tmp.color = new Color(0.5f, 0f, 0f, 1f);
            mute = true;
        }
        else if (mute)
        {
            AudioListener.volume = 1f;
            UnityEngine.UI.Text tmp = GetComponentInChildren<UnityEngine.UI.Text>();
            if (tmp)
                tmp.color = new Color(1f, 1f, 1f, 1f);
            mute = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneToLoad != "none" && soundPlayer.time > 1.3f)
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
    }
}
