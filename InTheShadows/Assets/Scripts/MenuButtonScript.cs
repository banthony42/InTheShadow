using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonScript : MonoBehaviour
{

    public GameObject fade;
    public AudioClip buttonClic;
    public AudioClip buttonTransition;

    private string sceneToLoad = "none";
    private AudioSource soundPlayer;

    // Use this for initialization
    void Start()
    {
        soundPlayer = GetComponent<AudioSource>();
        soundPlayer.volume = 0.1f;
    }

    public void pointerEnter()
    {
        soundPlayer.PlayOneShot(buttonTransition);
    }

    public void onClickButton(string button)
    {
        soundPlayer.volume = 0.5f;
        soundPlayer.clip = buttonClic;
        soundPlayer.Play();
        if (button == "test")
            UserSave.userP.setDebug(1);
        Debug.Log(UserSave.userP.getDebug());
        sceneToLoad = "LevelSelect";
        fade.GetComponent<UnityEngine.UI.RawImage>().raycastTarget = true;
        fade.GetComponent<FadeScript>().target = Color.black;
        fade.GetComponent<FadeScript>().timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneToLoad != "none" && soundPlayer.time > 1.3f)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
        }
    }
}
