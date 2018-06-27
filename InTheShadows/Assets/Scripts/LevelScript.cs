using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour {

    public GameObject myParticleLink;
    public string levelName;
    public int levelIndex;
    public Color unlockColor;

    [HideInInspector] public bool unlocked = false;
    private ParticleSystem myParticle;
    private Animator myAnim;
    private Color savedColor;
	// Use this for initialization
	void Start () {
        myAnim = GetComponent<Animator>();
        if (myParticleLink)
        {
            myParticle = myParticleLink.GetComponent<ParticleSystem>();
            savedColor = myParticle.startColor;
        }
	}
	
	// Update is called once per frame
	void Update () {

       // temporaire 
        if (Input.GetKey("space"))
        {
            unlocked = false;
            myAnim.SetBool("levelUnlocked", false);
            StopCoroutine("ParticleColorChange");
            myAnim.Play("Box_Idle");
            if (myParticleLink)
                myParticle.startColor = savedColor;
        }

        if (unlocked)
        {
            if (!myAnim.GetBool("levelUnlocked"))
                myAnim.SetBool("levelUnlocked", true);

            if (myParticleLink && myParticleLink.activeSelf && !myParticle.isPlaying)
                myParticle.Play();
            else if (myParticleLink)
                StartCoroutine("ParticleColorChange");
        }
	}

    IEnumerator ParticleColorChange()
    {
        yield return new WaitForSeconds(2f);
        myParticle.startColor = Color.Lerp(myParticle.startColor, unlockColor, Time.deltaTime);
    }
}

