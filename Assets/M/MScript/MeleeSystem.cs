using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMeleeSystem : MonoBehaviour {
    public static bool clicked = false;
    private PigController Pigscript;
    private WeaponNameController WeaponNameControl;
    public static int power = 5;
    // Use this for initialization
    private GameObject target;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter (Collider other)
    {
        if (clicked)
        {
            Debug.Log("Clicked");
            target = other.gameObject;
            ApplyDamage(target);
            Debug.Log(target);
        }
    }

    public void ApplyDamage(GameObject other)
    {
        if (other.tag == "PigTag")
        {

            Pigscript = (PigController)other.GetComponent(typeof(PigController));
            Pigscript.HpController(power);

        }
        if (other.tag == "LionTag")
        {

            Pigscript = (PigController)other.GetComponent(typeof(PigController));
            Pigscript.HpController(power);

        }
    }
}
