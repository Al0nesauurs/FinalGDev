using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTreeSpawn : MonoBehaviour {
    public GameObject Treemodel;
    public int number = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectsWithTag("TreeTag").Length < number)
        {
            Vector3 position = new Vector3(Random.Range(-7f, 8.5f), 5, Random.Range(0, 10f));
            var myNew= Instantiate(Treemodel, position, Quaternion.identity);
            myNew.transform.parent = gameObject.transform;
        }
    }
}
