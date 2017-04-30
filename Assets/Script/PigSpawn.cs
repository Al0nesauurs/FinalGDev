using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PigSpawn : NetworkBehaviour {
    public GameObject PigModel;
    public static int number = 0;
    public static bool start = true;

	void Update () {

        if(GameObject.FindGameObjectsWithTag("PigTag").Length<number&&start)
        {
            Vector3 position = new Vector3(Random.Range(-7f, 8.5f), 5, Random.Range(0,10f));
            var myNew = Instantiate(PigModel, position, Quaternion.identity);
            myNew.transform.parent = gameObject.transform;
            NetworkServer.Spawn(myNew);
        }
        else
        {
            PigSpawn.start = false;
        }
    }
}
