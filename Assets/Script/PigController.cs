﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour {
    Transform playerarm;
    float trun = 0;
    private float hp = 12;
    public static float damageApply = 0;
    bool running = false;
    bool fliping = false;
    float TimetoWalk = -10;
    public GameObject meat;
    public float pigspeed = 0.01f;
   
	// Use this for initialization
	void Start () {
        playerarm = GameObject.Find("PlayerArm").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update () {
        if (running)
        {
            run(trun += Time.deltaTime);
        }
        else
        {
            if (TimetoWalk<=0)
            {
                if (!fliping)
                {
                    Vector3 wayPoint = Random.insideUnitCircle * 100;
                    wayPoint.y = gameObject.transform.position.y;
                    gameObject.transform.LookAt(wayPoint);
                    fliping = true;
                }


                gameObject.transform.Translate(Vector3.forward * pigspeed);
                TimetoWalk -= Time.deltaTime;
                if (TimetoWalk <= -5)
                {
                    fliping = false;
                    TimetoWalk = Random.Range(10, 15);
                }
            }
            else
            {
               TimetoWalk -= Time.deltaTime;
            }
        }

	}

    public void HpController(int damage)
    {
        Debug.Log("HP CON!");
        running = true;
        fliping = true;
        gameObject.transform.Translate(Vector3.up * Time.deltaTime * 10f);
        hp -= damage;

        if (hp <= 0)
        {
            DropItem();
            DropItem();
            DropItem();
            Destroy(gameObject);
        }
    }
    void run(float timerun)
    {

        if (timerun >= 3)
        {
            running = false;
            trun = 0;
        }
        var targetPosition = playerarm.position;
        targetPosition.y = transform.position.y;
        gameObject.transform.LookAt(targetPosition);
        gameObject.transform.Rotate(0, 180, 0);
        gameObject.transform.Translate(Vector3.forward  * pigspeed*2);

    }
    void DropItem ()
    {
        Vector3 meatposition = new Vector3(Random.Range(gameObject.transform.position.x+0.3f, gameObject.transform.position.x -0.3f), gameObject.transform.position.y, gameObject.transform.position.z);
        Instantiate(meat, meatposition, Quaternion.identity);
    }
        
}
