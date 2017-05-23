using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {


    UnityEngine.AI.NavMeshAgent nav;
    GameObject player;
    Transform playertran;
    float trun = 0;
    public int hp = 1000;
    int beforehp = 1000;
    public static float damageApply = 0;
    bool running = false;
    bool fliping = false;
    float TimetoWalk = -10;
    public float speedup = 0;
    public GameObject meat;
    public GameObject slaves;
    public float lionspeed = 0.01f;
    int fieldOfViewRange = 45;


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playertran = player.transform;

        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            run();
        }
        else
        {
            if (CanSeePlayer())
            {
                Debug.Log("Detected!!!");
                running = true;
            }
            if (TimetoWalk <= 0)
            {
                if (!fliping)
                {
                    Vector3 wayPoint = new Vector3(Random.Range(-100, 100), gameObject.transform.position.y, Random.Range(-100, 100));
                    gameObject.transform.LookAt(wayPoint);
                    fliping = true;
                }


                gameObject.transform.Translate(Vector3.forward * lionspeed * Time.deltaTime * 10);
                TimetoWalk -= Time.deltaTime;
                if (TimetoWalk <= -5)
                {
                    fliping = false;
                    TimetoWalk = Random.Range(10, 15);
                }
            }
            else
            {
                TimetoWalk -= Time.deltaTime + speedup;
            }
        }

    }

    public void HpController(int damage)
    {
        Debug.Log("HP Boss = "+hp);
        running = true;
        fliping = true;
        hp -= damage;
        
        if(beforehp==hp+100)
        {
            Debug.Log(beforehp);
            spawnslaves(15-beforehp/100);
            beforehp -= 100;
        }
        if (hp <= 0)
        {
            DropItem();
            DropItem();
            DropItem();
            Destroy(gameObject);
        }
    }

    void run()
    {
        var targetPosition = playertran.position;
        nav.SetDestination(targetPosition);
    }

    void DropItem()
    {
        Vector3 meatposition = new Vector3(Random.Range(gameObject.transform.position.x + 0.3f, gameObject.transform.position.x - 0.3f), gameObject.transform.position.y, gameObject.transform.position.z);
        Instantiate(meat, meatposition, Quaternion.identity);
    }

    void spawnslaves (int num)
    {
        SlaveSpawn.start = true;
        SlaveSpawn.number = num;
        LionController.bosscommand = true;
    }
    bool CanSeePlayer()
    {
        var rayDirection = player.transform.position - transform.position;
        RaycastHit hit;
        int layerMask = 1 << 10;
        layerMask = ~layerMask;
        if (Physics.Raycast(transform.position, rayDirection, out hit, 7, layerMask))
        { 
            if (hit.transform.tag == "Player")
            {
                return true;
            }
        }
        if ((Vector3.Angle(rayDirection, transform.forward)) <= fieldOfViewRange)
        {
            if (Physics.Raycast(transform.position, rayDirection, out hit, 20, layerMask))
            {
                if (hit.transform.tag == "Player")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }
}
