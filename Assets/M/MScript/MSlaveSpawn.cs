using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSlaveSpawn : MonoBehaviour {
    public GameObject LionModel;
    public static int number = 0;
    public static bool start = true;

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("LionTag").Length < number && start)
        {
            Vector3 position = new Vector3(Random.Range(-10f, 10f) + gameObject.transform.position.x, 5, Random.Range(-0, 10f) + gameObject.transform.position.z);
            var myNew = Instantiate(LionModel, position, Quaternion.identity);
            myNew.transform.parent = gameObject.transform;
        }
        else
        {
            MSlaveSpawn.start = false;
        }
    }
}
