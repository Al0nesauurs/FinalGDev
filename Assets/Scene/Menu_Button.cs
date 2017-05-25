using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Button : MonoBehaviour {

    public void LoadSingleplayer()
    {
        SceneManager.LoadScene("Local");
    }
    public void LoadTutorial()
    {
        SceneManager.LoadScene("How2Play");
    }
    public void LoadMultiplayer()
    {
        SceneManager.LoadScene("Multiplayer");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("HuntFirebase");
    }
}
