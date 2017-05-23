using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletmove : MonoBehaviour {
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
        t += Time.deltaTime;
        if (t > 5)
            Destroy(gameObject);
		gameObject.transform.Translate(Vector3.up * Time.deltaTime * 10f*speed);
       /* Ray fRay = new Ray(transform.position, Vector3.forward);

        if (Physics.Raycast(fRay, out hit,1.5f))
        {

        }*/
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
