using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour {
    public float fadeTime;

    private float timer = 0f;
    private UnityEngine.UI.RawImage rawImage;
	// Use this for initialization
	void Start () {
        rawImage = GetComponent<UnityEngine.UI.RawImage>();
	}
	
	// Update is called once per frame
	void Update () {
        rawImage.color = Color.Lerp(rawImage.color, Color.clear, timer);
        if (timer < 1)
        {
            timer += Time.deltaTime / fadeTime;
        }
        if (rawImage.color == Color.clear)
            gameObject.SetActive(false);
	}
}
