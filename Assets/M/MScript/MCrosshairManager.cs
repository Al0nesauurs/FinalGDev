using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MCrosshairManager : MonoBehaviour {
    float ReloadTime = 0;
    bool Reloading = false;
    Text text;
    public GameObject player;
    void Start()
    {
        text = GetComponent<Text>();
    }


    void Update()
    {
        if (Reloading)
        {
            if (ReloadTime < 3)
            {
                ReloadTime += Time.deltaTime;
                text.text = "RELOADING IN " + (int)(4 - ReloadTime) + "";
            }
            else
            {
                text.text = "+";
                Reloading = false;
                ReloadTime = 0;
                player.GetComponent<MPlayerController>().CmdReload();
            }
        }
    }
    public void Haveammo()
    {
        text.text = "+";
    }
    public void initGun()
    {
        text.text = "Reload first";
    }
    public void Reload()
    {
        Reloading = true;
    }
    
    public void Noammo()
    {
        text.text = "Reload Now!! ";
    }
    
    public void DEATH()
    {
        text.text = "You are DEAD!!!";
    }
}
