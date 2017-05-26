using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MScoreShow : MonoBehaviour {

    Text text;
    void Start()
    {
        gameObject.GetComponent<Text>().text = MScoreManager.Score + "";
    }

}
