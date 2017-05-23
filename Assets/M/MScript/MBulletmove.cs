using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MBulletmove : NetworkBehaviour {
    private float t;
    private MPigController Pigscript;
    private MLionController Lionscript;
    private MPlayerController Playerscript;
    private MBossController Bossscript;
    private MWeaponNameController WeaponNameControl;
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
		gameObject.transform.Translate(Vector3.up * Time.deltaTime * 10f*speed);
	}


    public void ApplyDamage(GameObject other)
    {
        Debug.Log("Bullet hit -> "+other);
        if (other.tag == "PigTag")
        {
            Pigscript = (MPigController)other.GetComponent(typeof(MPigController));
            Pigscript.HpController(power);
        }
        if (other.tag == "LionTag")
        {
            Lionscript = (MLionController)other.GetComponent(typeof(MLionController));
            Lionscript.HpController(power);
        }
        if (other.tag == "BossTag")
        {
            Bossscript = (MBossController)other.GetComponent(typeof(MBossController));
            Bossscript.HpController(power);
        }
        if (other.tag == "Player")
        {
            Playerscript = (MPlayerController)other.GetComponent(typeof(MPlayerController));
            Playerscript.TakeDamage(power);
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
