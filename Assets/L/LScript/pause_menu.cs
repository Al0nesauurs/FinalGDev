using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause_menu : MonoBehaviour
{
	public AudioSource pause;
	public AudioSource normalsound;

    public void Resume()
    {
        if (PlayerController.PlayerHealth > 0)
        { 
            Time.timeScale = 1;
            GameObject.Find("PauseMenu").GetComponent<Canvas>().enabled = false;
            pause.enabled = false;
            normalsound.enabled = true;
            if (!PlayerController.CursorResume)
                Cursor.visible = false;
        }
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
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




}
