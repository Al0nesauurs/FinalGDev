using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawn : MonoBehaviour {
    public GameObject Stonemodel;
    public int number = 5;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("StoneTag").Length < number)
        {
            Vector3 position = new Vector3(Random.Range(-7f, 8.5f), 5, Random.Range(10, 11f));
            var myNew = Instantiate(Stonemodel, position, Quaternion.identity);
            myNew.transform.parent = gameObject.transform;
        }
    }
}
