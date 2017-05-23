using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int CountEnemy;


    Text text;


    void Awake ()
    {
        text = GetComponent <Text> ();
        CountEnemy = 0;
    }


    void Update ()
    {
        text.text = "Enemy left " + (GameObject.FindGameObjectsWithTag("PigTag").Length 
            + GameObject.FindGameObjectsWithTag("LionTag").Length
            + GameObject.FindGameObjectsWithTag("BossTag").Length);
    }
}
