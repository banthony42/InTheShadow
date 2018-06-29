using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSave : MonoBehaviour {

    public enum levelName
    {
        TEAPOT,
        ELEPHANT,
        SOMBRERO,
        H2G2,
        EARTH,
        SIZE,
    };

    [HideInInspector] public static UserSave userP;

    private int debug;
    private int[] levelUnlock;

    void Awake()
    {
        if (userP == null)
            userP = this;
    }

    // Use this for initialization
    void Start () {
        levelUnlock = new int[(int)levelName.SIZE];
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Menu")
            UserSave.userP.setDebug(0);
        LoadUserPref();
	}

    public void setDebug(int state)
    {
        debug = state;
        PlayerPrefs.SetInt("debug", debug);
        PlayerPrefs.Save();
    }

    public bool getDebug()
    {
        return (debug > 0);
    }

    // Charge les player pref dans les variables
    public void LoadUserPref()
    {
        levelUnlock[(int)levelName.TEAPOT] = PlayerPrefs.GetInt("T-Time");
        levelUnlock[(int)levelName.ELEPHANT] = PlayerPrefs.GetInt("Wild Trumpet");
        levelUnlock[(int)levelName.SOMBRERO] = PlayerPrefs.GetInt("Aie, Pepito !");
        levelUnlock[(int)levelName.H2G2] = PlayerPrefs.GetInt("H2G2");
        levelUnlock[(int)levelName.EARTH] = PlayerPrefs.GetInt("Blue Planet");
        debug = PlayerPrefs.GetInt("debug");
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
        PlayerPrefs.SetInt("T-Time", levelUnlock[(int)levelName.TEAPOT]);
        PlayerPrefs.SetInt("Wild Trumpet", levelUnlock[(int)levelName.ELEPHANT]);
        PlayerPrefs.SetInt("Aie, Pepito !", levelUnlock[(int)levelName.SOMBRERO]);
        PlayerPrefs.SetInt("H2G2", levelUnlock[(int)levelName.H2G2]);
        PlayerPrefs.SetInt("Blue Planet", levelUnlock[(int)levelName.EARTH]);
        PlayerPrefs.Save();
    }

    public bool getState(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < (int)levelName.SIZE)
            return (levelUnlock[levelIndex] > 0);
        return false;
    }

    public void setState(int levelIndex, int state)
    {
        if (levelIndex >= 0 && levelIndex < (int)levelName.SIZE)
        {
            levelUnlock[levelIndex] = state;
            UpdatePref();
        }
    }
}
