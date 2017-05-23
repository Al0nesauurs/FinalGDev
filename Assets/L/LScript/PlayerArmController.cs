using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmController : MonoBehaviour {
    private float t=0;
    private bool hit = false;
    private Transform ArmLocate;
	// Use this for initialization
	void Start () {
        ArmLocate = GameObject.Find("ArmPosition").transform;

    }
	
	// Update is called once per frame
	void Update () {

        if(hit==true)
        {
            t += Time.deltaTime;
            if (t <= 0.4)
            {
                MeleeSystem.clicked = true;
                gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 1f);
            }
            if (t >= 0.4 )
            {
                MeleeSystem.clicked = false;
                gameObject.transform.Translate(Vector3.back * Time.deltaTime * 1f);
            }
            if (t >= 0.8)
            {
                gameObject.transform.position = ArmLocate.transform.position;
                hit = false;
                t = 0;
            }
         }


    }
}
