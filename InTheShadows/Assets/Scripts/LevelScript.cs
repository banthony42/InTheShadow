using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour {

    public GameObject myParticleLink;
    public string levelName;
    public int levelIndex;
    public Color unlockColor;

    private ParticleSystem myParticle;
    private Animator myAnim;
    private Color savedColor;
    private bool hold = false;

	// Use this for initialization
	void Start () {
        hold = false;
        myAnim = GetComponent<Animator>();
        if (myParticleLink)
        {
            myParticle = myParticleLink.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule psmain = myParticle.main;
            savedColor = psmain.startColor.color;
        }
	}
	
	// Update is called once per frame
	void Update () {

        // Si le niveau correspondant au levelName est debloque
        if ((UserSave.userP.getState(levelIndex) || UserSave.userP.getDebug()) && !hold)
        {
            myAnim.SetBool("levelUnlocked", true);

            if (myParticleLink && myParticleLink.activeSelf && !myParticle.isPlaying)
                myParticle.Play();
            else if (myParticleLink)
                StartCoroutine("ParticleColorChange");

                if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("LevelUnlocked"))
                {
                    myAnim.SetBool("levelUnlocked", false);
                    myAnim.SetBool("UnlockIdle", true);
                    hold = true;
                }
        }

        if (!UserSave.userP.getState(levelIndex) && hold && !UserSave.userP.getDebug())
        {
            myAnim.SetBool("UnlockIdle", false);
            myAnim.Play("Box_Idle");
            if (myParticleLink)
            {
                ParticleSystem.MainModule psmain = myParticle.main;
                psmain.startColor = savedColor;
                StopCoroutine("ParticleColorChange");
            }
            hold = false;
        }
	}

    IEnumerator ParticleColorChange()
    {
        yield return new WaitForSeconds(0.3f);
        ParticleSystem.MainModule psmain = myParticle.main;
        psmain.startColor = Color.Lerp(psmain.startColor.color, unlockColor, Time.deltaTime);
        if (psmain.startColor.color == unlockColor)
            StopCoroutine("ParticleColorChange");
        else
            StartCoroutine("ParticleColorChange");
    }
}