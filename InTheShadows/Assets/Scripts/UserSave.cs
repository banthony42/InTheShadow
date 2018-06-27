using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSave : MonoBehaviour {

    [HideInInspector] public int teapotUnlock;
    [HideInInspector] public int elephantUnlock;
    [HideInInspector] public int sombreroUnlock;
    [HideInInspector] public int fortyTwoUnlock;
    [HideInInspector] public int earthGlobeUnlock;

    [HideInInspector] public static UserSave userP;

    void Awake()
    {
        if (userP == null)
            userP = this;
    }

    // Use this for initialization
    void Start () {
        LoadUserPref();
	}
	
    // Charge les player pref dans les variables
    public void LoadUserPref()
    {   
        teapotUnlock = PlayerPrefs.GetInt("teapot");
        elephantUnlock = PlayerPrefs.GetInt("elephant");
        sombreroUnlock = PlayerPrefs.GetInt("sombrero");
        fortyTwoUnlock = PlayerPrefs.GetInt("fortyTwo");
        earthGlobeUnlock = PlayerPrefs.GetInt("earthGlobe");
    }

    // Reset le contenu des player pref
    // Puis charge le tout dans les variables
    public void ResetPref()
    {
        PlayerPrefs.SetInt("teapot", 0);
        PlayerPrefs.SetInt("elephant", 0);
        PlayerPrefs.SetInt("sombrero", 0);
        PlayerPrefs.SetInt("eartchGlobe", 0);
        LoadUserPref();
    }

    // Met a jour les player pref avec le contenu des variables
    // Puis sauvegarde les player pref
    public void UpdatePref()
    {
        PlayerPrefs.SetInt("teapot", teapotUnlock);
        PlayerPrefs.SetInt("elephant", elephantUnlock);
        PlayerPrefs.SetInt("sombrero", sombreroUnlock);
        PlayerPrefs.SetInt("eartchGlobe", earthGlobeUnlock);
        PlayerPrefs.Save();
    }
}
