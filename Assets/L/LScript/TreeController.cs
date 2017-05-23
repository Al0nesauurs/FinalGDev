using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour {
    private float hp = 5;
    public GameObject wood;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {


    }


    public void HpController(int damage)
    {

        hp -= damage;
        if (hp <= 0)
            if (hp <= 0)
            {
                DropItem();
                DropItem();
                Destroy(gameObject);
            }
    }
    void DropItem()
    {
        Vector3 woodposition = new Vector3(Random.Range(gameObject.transform.position.x + 0.3f, gameObject.transform.position.x - 0.3f), gameObject.transform.position.y-0.5f, gameObject.transform.position.z);
        Instantiate(wood, woodposition, Quaternion.identity);
    }

}
