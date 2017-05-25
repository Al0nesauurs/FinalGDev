using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mpause_menu : MonoBehaviour
{
	public AudioSource pause;
	public AudioSource normalsound;

    public void Resume()
    {
        if (!MPlayerController.dying)
        { 
            Time.timeScale = 1;
            GameObject.Find("PauseMenu").GetComponent<Canvas>().enabled = false;
            pause.enabled = false;
            normalsound.enabled = true;
            if (!MPlayerController.CursorResume)
                Cursor.visible = false;
        }
    }

    public void Score()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("HuntFirebase");
    }

    public void ShowKeyControl()
    {
        GameObject.Find("KeyboardControl").GetComponent<Canvas>().enabled = true;
    }

    public void Back()
    {
        GameObject.Find("KeyboardControl").GetComponent<Canvas>().enabled = false;
    }

	public void Audio()
	{
		GameObject.Find("option").GetComponent<Canvas>().enabled = true;
	}

	public void BackAudio()
	{
		GameObject.Find("option").GetComponent<Canvas>().enabled = false;
	}

    public void Close()
    {
        GameObject.Find("PauseMenu").GetComponent<Canvas>().enabled = true;
    }




}
