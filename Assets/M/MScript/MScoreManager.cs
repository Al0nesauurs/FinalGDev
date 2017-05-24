using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MScoreManager : MonoBehaviour
{
    public static int Score;

    Text text;
    void Start ()
    {
        text = GetComponent <Text> ();
    }


    void Update ()
    {
        if(!MPlayerController.dying)
            text.text = "Score : " + (Score);
    }
}
