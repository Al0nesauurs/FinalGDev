using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MBossController : NetworkBehaviour {


    UnityEngine.AI.NavMeshAgent nav;
    float trun = 0;
    [SyncVar]public int hp = 1000;
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
            MLionController.bosscommand = false;
            MScoreManager.Score += 100;
            Destroy(gameObject);
        }
    }

    void run()
    {
        var targetPosition = FindClosestEnemy().transform.position;
        nav.SetDestination(targetPosition);
    }

    void DropItem()
    {
        Vector3 meatposition = new Vector3(Random.Range(gameObject.transform.position.x + 0.3f, gameObject.transform.position.x - 0.3f), gameObject.transform.position.y, gameObject.transform.position.z);
        GameObject Dmeat = (GameObject)Instantiate(meat, meatposition, Quaternion.identity);
        NetworkServer.Spawn(Dmeat);
    }

    void spawnslaves (int num)
    {
        MSlaveSpawn.start = true;
        MSlaveSpawn.number = num;
        MLionController.bosscommand = true;
    }
    bool CanSeePlayer()
    {
        var rayDirection = FindClosestEnemy().transform.position - transform.position;
        RaycastHit hit;
        int layerMask = 1 << 10;
        layerMask = ~layerMask;
        if (Physics.Raycast(transform.position, rayDirection, out hit, 7, layerMask))
        { // If the player is very close behind the player and in view the enemy will detect the player
          //Debug.Log(hit.transform.name);
            if (hit.transform.tag == "Player")
            {
                //Debug.Log("Close");
                return true;
            }
        }
        if ((Vector3.Angle(rayDirection, transform.forward)) <= fieldOfViewRange)
        {
            // Detect if player is within the field of view
            if (Physics.Raycast(transform.position, rayDirection, out hit, 20, layerMask))
            {
                //Debug.Log((Vector3.Angle(rayDirection, transform.forward)));
                if (hit.transform.tag == "Player")
                {
                    //Debug.Log("Can see player");
                    return true;
                }
                else
                {
                    //Debug.Log("Can not see player");
                    return false;
                }
            }
        }
        return false;
    }
    GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
