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
        teapotUnlock = PlayerPrefs.GetInt("T-Time");
        elephantUnlock = PlayerPrefs.GetInt("Wild Trumpet");
        sombreroUnlock = PlayerPrefs.GetInt("Aie, Pepito !");
        fortyTwoUnlock = PlayerPrefs.GetInt("H2G2");
        earthGlobeUnlock = PlayerPrefs.GetInt("Blue Planet");
    }

    // Reset le contenu des player pref
    // Puis charge le tout dans les variables
    public void ResetPref()
    {
        PlayerPrefs.SetInt("T-Time", 0);
        PlayerPrefs.SetInt("Wild Trumpet", 0);
        PlayerPrefs.SetInt("Aie, Pepito !", 0);
        PlayerPrefs.SetInt("H2G2", 0);
        PlayerPrefs.SetInt("Blue Planet", 0);
        LoadUserPref();
    }

    // Met a jour les player pref avec le contenu des variables
    // Puis sauvegarde les player pref
    public void UpdatePref()
    {
        PlayerPrefs.SetInt("T-Time", teapotUnlock);
        PlayerPrefs.SetInt("Wild Trumpet", elephantUnlock);
        PlayerPrefs.SetInt("Aie, Pepito !", sombreroUnlock);
        PlayerPrefs.SetInt("H2G2", fortyTwoUnlock);
        PlayerPrefs.SetInt("Blue Planet", earthGlobeUnlock);
        PlayerPrefs.Save();
    }

    public bool getState(string levelName)
    {
        return (PlayerPrefs.GetInt(levelName) > 0);
    }

    public void setState(string levelName, int state)
    {
        if (levelName == "T-Time")
            teapotUnlock = state;
        else if (levelName == "Wild Trumpet")
            elephantUnlock = state;
        else if (levelName == "Aie, Pepito !")
            sombreroUnlock = state;
        else if (levelName == "H2G2")
            fortyTwoUnlock = state;
        else if (levelName == "Blue Planet")
            earthGlobeUnlock = state;
        UpdatePref();
    }
}
