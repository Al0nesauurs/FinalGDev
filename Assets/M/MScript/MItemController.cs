using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MItemController : MonoBehaviour {

    private float hp = 1;
    private float t = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > 10)
            Destroy(gameObject);
    }
    public void HpController(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            Destroy(gameObject);
    }
}
