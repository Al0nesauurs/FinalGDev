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
    public GameObject mycrosshair;
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



    

   

    }


}

