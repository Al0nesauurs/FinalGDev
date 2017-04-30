using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bulletmove : NetworkBehaviour {
    private float t;
    private PigController Pigscript;
    private LionController Lionscript;
    private BossController Bossscript;
    private WeaponNameController WeaponNameControl;
    public static int power = 10;
    public int speed = 1;
    RaycastHit hit;
	// Update is called once per frame
	void Update () {
        if(isLocalPlayer)
        {
            return;
        }

        t += Time.deltaTime;
        if (t > 5)
            Destroy(gameObject);
		gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 10f*speed);
	}


    public void ApplyDamage(GameObject other)
    {
        Debug.Log("Bullet hit -> "+other);
        if (other.tag == "PigTag")
        {
            Pigscript = (PigController)other.GetComponent(typeof(PigController));
            Pigscript.HpController(power);
        }
        if (other.tag == "LionTag")
        {
            Lionscript = (LionController)other.GetComponent(typeof(LionController));
            Lionscript.HpController(power);
        }
        if (other.tag == "BossTag")
        {
            Bossscript = (BossController)other.GetComponent(typeof(BossController));
            Bossscript.HpController(power);
        }
        if (other.tag!="ItemTag")
         Destroy(gameObject);

    }
    public void OnTriggerEnter (Collider other )
    {
        Debug.Log("Bullet hit -> " + other.gameObject);

        ApplyDamage(other.gameObject);

    }
}
