using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Takeitem : MonoBehaviour {
	

	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject.tag == "Player"&&PlayerController.Cantakeitem)
		{
            Cursor.visible = false;
			WeaponNameController.weaponname = gameObject.name;
			Destroy(gameObject);
		}
			
	}
}
