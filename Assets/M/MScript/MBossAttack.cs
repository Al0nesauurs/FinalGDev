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


    void Start()
    {
        player = GameObject.Find("MPlayer");
        playerarm = GameObject.Find("PlayerArm");
        playerctl = player.GetComponent<MPlayerController>();
        bossctrl = GetComponent<MBossController>();
        //anim = GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject == player)
        {
            // ... the player is in range.
            Debug.Log("Boss hitting " + other.gameObject);
            playerInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        // If the exiting collider is the player...
        if (other.gameObject == playerarm)
        {
            // ... the player is no longer in range.
            playerInRange = false;
        }
    }


    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (timer >= timeBetweenAttacks && playerInRange && bossctrl.hp > 0)
        {
            // ... attack.
            Attack();
        }
    }

    void Attack()
    {
        // Reset the timer.
        timer = 0f;

        // If the player has health to lose...
        if (!MPlayerController.dying)
        {
            // ... damage the player.
            playerctl.TakeDamage(attackDamage);
        }
    }
}
