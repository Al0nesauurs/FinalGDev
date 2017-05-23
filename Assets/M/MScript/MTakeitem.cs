using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTakeitem : MonoBehaviour {
	

	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject.tag == "Player"&&MPlayerController.Cantakeitem)
		{
            Cursor.visible = false;
			MWeaponNameController.weaponname = gameObject.name;
			Destroy(gameObject);
		}
			
	}
}
