using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    public int attackDamage = 10;               // The amount of health taken away per attack.


    //Animator anim;                              // Reference to the animator component.
	GameObject player;                          // Reference to the player GameObject.
    GameObject playerarm;
    PlayerController playerctl;               // Reference to the player's health.
    LionController lionctl;
    BossController bossctrl;
    bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    float timer;                                // Timer for counting up to the next attack.


    void Start()
    {
		player = GameObject.Find("Player(Clone)");
        playerarm = GameObject.Find("PlayerArm");
		playerctl = player.GetComponent<PlayerController>();
        lionctl = GetComponent<LionController>();
        bossctrl = GetComponent<BossController>();
        //anim = GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject == player)
        {
            // ... the player is in range.
            Debug.Log("Lion hitting " + other.gameObject);
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
        if (timer >= timeBetweenAttacks && playerInRange && lionctl.hp > 0)
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
        if (PlayerController.PlayerHealth > 0)
        {
            // ... damage the player.
            playerctl.TakeDamage(attackDamage);
        }
    }
}