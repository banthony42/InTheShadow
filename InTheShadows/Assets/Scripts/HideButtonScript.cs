using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideButtonScript : MonoBehaviour {

    public UnityEngine.UI.Image resetButton;
    public UnityEngine.UI.Text resetButtonText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (UserSave.userP.getDebug() == true)
        {
            resetButtonText.color = new Color(resetButtonText.color.r, resetButtonText.color.g, resetButtonText.color.b, 0f);
            resetButton.raycastTarget = false;
            resetButtonText.raycastTarget = false;
        }
        else
        {
            resetButtonText.color = new Color(resetButtonText.color.r, resetButtonText.color.g, resetButtonText.color.b, 1f);
            resetButton.raycastTarget = true;
            resetButtonText.raycastTarget = true;
        }
	}
}
