using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class WeaponController : MonoBehaviour {

    public GameObject Bullet;
    public Transform BulletSpawn;
    public GameObject Pistol;
    public Transform ArmLocate;
    public static int ammo =0;
    float t = 0;
    bool hit = false;
    float timeReload = 0;
    public static bool startReload = false;
    //sound part
    public AudioClip HandgunSound;
    public AudioClip MachinegunSound;
    public AudioClip HandgunSoundR;
    public AudioClip MachinegunSoundR;
    private AudioSource source;
    //end sound part

    private GameObject muzzleFlash;
    private ParticleSystem flash;


    void Start () {
        t = 0;
        hit = false;
        timeReload = 0;
        startReload = false;
        source = GetComponent<AudioSource>();
	}

	
	// Update is called once per frame
	void Update () {

        if (hit)
        {
            t += Time.deltaTime;
            if (t <= 0.1f)
                MeleeSystem.clicked = true;

            if (t <= 0.4)
            {

                gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 1f);
            }
            if (t >= 0.4)
            {
                gameObject.transform.Translate(Vector3.back * Time.deltaTime * 1f);
            }
            if (t >= 0.8)
            {
                gameObject.transform.position = ArmLocate.transform.position;
                hit = false;
                t = 0;
            }
        }

    }

    public void Reload (ref int localammo)
    {
                GameObject.Find("Crosshair").GetComponent<Text>().text = "+";
                if (gameObject.name == "pistol" || gameObject.name == "pistol(Clone)")
                {
                    localammo = 10;
                }
                if (gameObject.name == "machinegun" || gameObject.name == "machinegun(Clone)")
                {
                    localammo = 30;
                }

            
    }

    public void CmdCheckWeapon(ref int ammolocal)
    {
        Debug.Log("CheckWEapon" + gameObject.name);
        if(gameObject.name=="hand")
        {
            hit = true;
        }
        else if (gameObject.name=="pistol"|| gameObject.name == "pistol(Clone)")
        {
            if (ammolocal > 0)
            {
                source.PlayOneShot(HandgunSound, 1F);
                GameObject bullet =(GameObject)Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
                muzzleFlash = GameObject.Find("Muzzle Flash p");
                flash = muzzleFlash.GetComponent<ParticleSystem>();
                flash.Play();
                NetworkServer.Spawn(bullet);
                ammolocal--;
            }
            if(ammolocal==0)
            {
                source.PlayOneShot(HandgunSoundR, 1F);
                GameObject.Find("Crosshair").GetComponent<Text>().text = "RELOAD NOW!!";
            }
        }
        else if (gameObject.name == "machinegun" || gameObject.name == "machinegun(Clone)")
        {
            if (ammo > 0)
            {
                source.PlayOneShot(MachinegunSound, 1F);
                Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
                muzzleFlash = GameObject.Find("Muzzle Flash m");
                flash = muzzleFlash.GetComponent<ParticleSystem>();
                flash.Play();
                ammo--;
            }
            if (ammo == 0)
            {
                source.PlayOneShot(MachinegunSoundR, 1F);
                //GameObject.Find("Crosshair").GetComponent<Text>().text = "RELOAD NOW!!";
            }
        }

    }


}

