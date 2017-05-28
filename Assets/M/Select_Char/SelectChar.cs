using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SelectChar : MonoBehaviour
{


    public void LoadSelectGame1()
    {
        SceneManager.LoadScene("SelectGame");
        MPlayerController.selectchar = 1;
        PlayerController.selectchar = 1;
    }
    public void LoadSelectGame2()
    {
        SceneManager.LoadScene("SelectGame");
        MPlayerController.selectchar = 2;
        PlayerController.selectchar = 21;
    }
    public void LoadSelectGame3()
    {
        SceneManager.LoadScene("SelectGame");
        MPlayerController.selectchar = 3;
        PlayerController.selectchar = 3;
    }
    public void LoadSelectGame4()
    {
        SceneManager.LoadScene("SelectGame");
        MPlayerController.selectchar = 4;
        PlayerController.selectchar = 4;
    }
    public void LoadSelectGame5()
    {
        SceneManager.LoadScene("SelectGame");
        MPlayerController.selectchar = 5;
        PlayerController.selectchar = 5;
    }
}