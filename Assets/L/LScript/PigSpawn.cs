using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigSpawn : MonoBehaviour {
    public GameObject PigModel;
    public float AllTime = 0;
    public static int number = 0;
    public static bool start = true;

	void Update () {
		AllTime += Time.deltaTime;
       // Debug.Log("number = " +number);
        //Debug.Log(GameObject.FindGameObjectsWithTag("PigTag").Length);

        if(GameObject.FindGameObjectsWithTag("PigTag").Length<number&&start)
        {
            Vector3 position = new Vector3(Random.Range(-7f, 8.5f), 5, Random.Range(0,10f));
            var myNew = Instantiate(PigModel, position, Quaternion.identity);
            myNew.transform.parent = gameObject.transform;
        }
        else
        {
            PigSpawn.start = false;
        }
    }
}
