using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    public float fadeTime;
    public Animator titleAnim;

    [HideInInspector] public float timer = 0f;
    [HideInInspector] public Color target = Color.clear;
    [HideInInspector] public bool enable = true;
    private UnityEngine.UI.RawImage rawImage;
    private float titleTimer;
    // Use this for initialization
    void Start()
    {
        titleTimer = 0;
        rawImage = GetComponent<UnityEngine.UI.RawImage>();
    }

    public void fadeOut()
    {
        target = Color.black;
        timer = 0f;
        rawImage.raycastTarget = true;
    }

    // Update is called once per frame
    void Update()
    {
        rawImage.color = Color.Lerp(rawImage.color, target, timer);
        if (timer < 1)
            timer += Time.deltaTime / fadeTime;
        if (rawImage.color.a < 0.1f && enable)
        {
            rawImage.raycastTarget = false;
            enable = false;
        }

        if (enable == false && titleAnim)
        {
            if (titleTimer > 2f)
                titleAnim.SetTrigger("TitleFadeOut");
            else
                titleTimer += Time.deltaTime / 1f;
        }
    }
}
