using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class MWeaponNameController : MonoBehaviour
{
    public GameObject Pistol;
    public GameObject MachineGun;
	public static string weaponname="hand";
	bool fix =true;
    bool parented=false;
    GameObject myNew;
    public GameObject crosshair;

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
            if (weaponname == "Mpistol" || weaponname == "Mpistol(Clone)")
            {
                crosshair.GetComponent<MCrosshairManager>().initGun();
                myNew = Instantiate(Pistol, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y-0.06f, gameObject.transform.position.z), gameObject.transform.rotation);
                MBulletmove.power = 10;
            }
            else if (weaponname == "Mmachinegun" || weaponname == "Mmachinegun(Clone)")
             {
                crosshair.GetComponent<MCrosshairManager>().initGun();
                myNew = Instantiate(MachineGun, gameObject.transform.position, gameObject.transform.rotation);
                MBulletmove.power = 20;
            }
            Debug.Log("Add Weapon" + weaponname);
			myNew.transform.parent = gameObject.transform;
            Cursor.visible = false;
			fix = false;
            parented = true;
		}

        if(weaponname=="Mpistol" && Input.GetKeyDown(KeyCode.G)|| weaponname == "Mpistol(Clone)" && Input.GetKeyDown(KeyCode.G) ||
            weaponname == "Mmachinegun" && Input.GetKeyDown(KeyCode.G) || weaponname == "Mmachinegun(Clone)"&& Input.GetKeyDown(KeyCode.G))
        {
                MPlayerController.HaveGun = true;
				MPlayerController.Cantakeitem = false;
                GameObject.Find("Crosshair").GetComponent<Text>().text = "+";
                MPlayerController.canrightclick = true;
                Debug.Log("DropWeapon");
                weaponname = "hand";
                parented = false;
                myNew.transform.parent = null;
                fix = true;
        }
        gameObject.name = weaponname;
	}
}
