using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelectLevelScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("escape"))
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu"); 
	}
}
