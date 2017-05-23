using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponNameController : MonoBehaviour
{
    public GameObject Pistol;
    public GameObject MachineGun;
	public static string weaponname="hand";
	bool fix =true;
    bool parented=false;
    GameObject myNew;

    void Start ()
    {
        fix = true;
        parented = false;
        weaponname = "hand";
    }
	void Update()
	{
        if (fix&&weaponname!="hand")
        {
            if (weaponname == "Lpistol" || weaponname == "Lpistol(Clone)")
            {
                GameObject.Find("Crosshair").GetComponent<Text>().text = "Reload first!!!";
                myNew = Instantiate(Pistol, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y-0.06f, gameObject.transform.position.z), gameObject.transform.rotation);
                WeaponController.ammo = 0;
                Bulletmove.power = 10;
            }
            else if (weaponname == "Lmachinegun" || weaponname == "Lmachinegun(Clone)")
             {
                GameObject.Find("Crosshair").GetComponent<Text>().text = "Reload first!!!";
                myNew = Instantiate(MachineGun, gameObject.transform.position, gameObject.transform.rotation);
                WeaponController.ammo = 0;
                Bulletmove.power = 20;
            }
            Debug.Log("Add Weapon" + weaponname);
			myNew.transform.parent = gameObject.transform;
            Cursor.visible = false;
			fix = false;
            parented = true;
		}

        if(weaponname=="Lpistol" && Input.GetKeyDown(KeyCode.G)|| weaponname == "Lpistol(Clone)" && Input.GetKeyDown(KeyCode.G) ||
            weaponname == "Lmachinegun" && Input.GetKeyDown(KeyCode.G) || weaponname == "Lmachinegun(Clone)"&& Input.GetKeyDown(KeyCode.G))
        {
				PlayerController.Cantakeitem = false;
                GameObject.Find("Crosshair").GetComponent<Text>().text = "+";
                PlayerController.canrightclick = true;
                Debug.Log("DropWeapon");
                weaponname = "hand";
                parented = false;
                myNew.transform.parent = null;
                fix = true;
        }
        gameObject.name = weaponname;
	}
}
