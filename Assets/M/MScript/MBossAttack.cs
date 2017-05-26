using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBossAttack : MonoBehaviour {

    public float timeBetweenAttacks = 0.2f;     // The time in seconds between each attack.
    public int attackDamage = 10;               // The amount of health taken away per attack.


    //Animator anim;                              // Reference to the animator component.
    GameObject player;                          // Reference to the player GameObject.
    GameObject playerarm;
    MPlayerController playerctl;               // Reference to the player's health.
    MLionController lionctl;
    MBossController bossctrl;
    bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    float timer;                                // Timer for counting up to the next attack.
    GameObject myobject;


    void Start()
    {
        player = GameObject.Find("MPlayer(Clone)");
        playerarm = GameObject.Find("PlayerArm");
        lionctl = GetComponent<MLionController>();
        bossctrl = GetComponent<MBossController>();
        //anim = GetComponent<Animator>();
    }



    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject.tag == "Player")
        {
            // ... the player is in range.
            Debug.Log("Boss hitting " + other.gameObject);
            myobject = other.gameObject;
            playerInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        // If the exiting collider is the player...
        if (other.gameObject.tag == "Player")
        {
            // ... the player is no longer in range.
            playerInRange = false;
        }
    }


    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks && playerInRange && bossctrl.hp > 0)
        {
            Attack();
        }
    }

    void Attack()
    {
        timer = 0f;
        if (!MPlayerController.dying)
        {
            myobject.GetComponent<MPlayerController>().TakeDamage(attackDamage);
        }
    }
}
