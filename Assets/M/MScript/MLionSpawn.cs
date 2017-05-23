using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MLionSpawn : NetworkBehaviour {

    public GameObject LionModel;
    public float AllTime = 0;
    public static int number = 0;
    public static bool start = true;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AllTime += Time.deltaTime;
        if (GameObject.FindGameObjectsWithTag("LionTag").Length < number && start)
        {
            Vector3 position = new Vector3(Random.Range(-10f, 10f) +gameObject.transform.position.x, 5, Random.Range(-0, 10f) + gameObject.transform.position.z);
            var myNew = Instantiate(LionModel, position, Quaternion.identity);
            myNew.transform.parent = gameObject.transform;
            NetworkServer.Spawn(myNew);

        }
        else
        {
            MLionSpawn.start = false;
        }
    }
}
