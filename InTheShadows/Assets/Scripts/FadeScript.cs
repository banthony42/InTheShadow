using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    public float fadeTime;

    [HideInInspector] public float timer = 0f;
    [HideInInspector] public Color target = Color.clear;
    [HideInInspector] public bool enable = true;
    private UnityEngine.UI.RawImage rawImage;
    // Use this for initialization
    void Start()
    {
        rawImage = GetComponent<UnityEngine.UI.RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        rawImage.color = Color.Lerp(rawImage.color, target, timer);
        if (timer < 1)
        {
            timer += Time.deltaTime / fadeTime;
        }
        if (rawImage.color.a < 0.1f && enable)
        {
            rawImage.raycastTarget = false;
            enable = false;
        }
    }
}
