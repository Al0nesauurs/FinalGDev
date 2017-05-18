using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class PlayerController : NetworkBehaviour
{
    private bool CanWalk = true;
    private bool CanJump = true;
    private bool usingitem = false;
    private bool Reloading = false;
    static public bool HaveGun = false;
    public static bool CursorResume = true;
    public static bool canrightclick = true;
    private float distToGround;
    public float force = 5;
    public float MouseSpeed = 3;
	public float WaitTime =3;
    private float ReloadTime = 0;
    float mouseInputX, mouseInputY;
    [SyncVar]public int PlayerHealth = 100;
    public static bool dying = false;
    public WeaponController WeaponControl;
    public WeaponNameController WeaponNameControl;
    public Camera Tps;
    public Camera Fps;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
	private double timer = 1.0;
	public AudioSource pause;
	public AudioSource normalsound;
	AudioSource soundEffect;
	public AudioClip liondeath;
	public static bool Cantakeitem = true;
    public int ammolocal=0;
    public GameObject HpUI;


    //port from WEAPONCONTROLLER.CS
    //sound part
    public AudioClip HandgunSound;
    public AudioClip MachinegunSound;
    public AudioClip HandgunSoundR;
    public AudioClip MachinegunSoundR;
    private AudioSource source;
    //end sound part
    public GameObject Bullet;
    public Transform BulletSpawn;
    public GameObject Pistol;
    public Transform ArmLocate;
    float t = 0;
    bool hit = false;
    float timeReload = 0;
    public static bool startReload = false;
    private GameObject muzzleFlash;
    private ParticleSystem flash;
    public GameObject mycrosshair;
    //END PORT

    void Start()
    {
        source = GetComponent<AudioSource>();
        Cursor.visible = false;
        CanWalk = true;
        CanJump = true;
        usingitem = false; 
        CursorResume = false;
        canrightclick = true;
        Time.timeScale = 1;
		soundEffect = GetComponent<AudioSource>();
        PlayerHealth = 100;
        Tps.enabled = true;
        Fps.enabled = false;
		Cantakeitem = true;
        distToGround = GameObject.Find("LegRight").GetComponent<Collider>().bounds.extents.y;
        gameObject.GetComponentInChildren<Canvas>().enabled = false;

    }
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        CheckIfLocal();
        if (!isLocalPlayer)
        {
            Tps.gameObject.SetActive(false);
            Fps.gameObject.SetActive(false);
            HpUI.SetActive(false);
            return;
        }
        else
        {
            CheckHaveGun();
            CheckTakeItem();
            if (PlayerHealth > 0)
            {
                KeyboardControl();
                MouseControl();

                if (Reloading)
                {
                    if (ReloadTime < 3)
                    {
                        ammolocal = 0;
                        ReloadTime += Time.deltaTime;
                        gameObject.GetComponentInChildren<Text>().text= "RELOADING IN " + (int)(4 - ReloadTime) + "";
                    }
                    else
                    {
                        CmdReload();
                        gameObject.GetComponentInChildren<Text>().text = "+";
                        Reloading = false;
                        ReloadTime = 0;
                    }
                }

            }
            UIControl();
        }

    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void CheckHaveGun()
    {
        if (HaveGun == false)
        {
            //HaveGun = true;
            WeaponNameController.weaponname = "pistol";
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void CheckIfLocal()
    {
       
        GameObject.Find("OfflineCam").GetComponent<Camera>().enabled = false;
        
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void MouseControl()
	{
		mouseInputY += Input.GetAxis("Mouse X") * MouseSpeed * Time.deltaTime * 20;
		mouseInputX -= Input.GetAxis("Mouse Y") * MouseSpeed * Time.deltaTime * 20;
		mouseInputX = Mathf.Clamp(mouseInputX, -80, 45);
		gameObject.transform.rotation = Quaternion.Euler(mouseInputX, mouseInputY, 0);
		if (Input.GetAxis("Mouse ScrollWheel") < 0 || Input.GetKeyDown(KeyCode.Z) && Tps.enabled == false)
		{
            gameObject.GetComponentInChildren<Canvas>().enabled = false;
			Tps.enabled = true;
			Fps.enabled = false;
		}
		else if (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetKeyDown(KeyCode.Z) && Tps.enabled == true)
		{
            gameObject.GetComponentInChildren<Canvas>().enabled = true;
			Tps.enabled = false;
			Fps.enabled = true;
		}
		if (Input.GetKeyDown(KeyCode.Mouse0)&&Time.timeScale == 1)
		{
            Debug.Log("SHOOTING");
            CmdCheckWeapon();
        }

	}
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /*
    void CmdShoot()
    {
        Debug.Log("before shoot ammo = " + ammolocal);
        CmdCheckWeapon();
        Debug.Log("After shoot ammo = "+ammolocal);
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [Command]
    void CmdReload()
    {
        ammolocal = 0;
        Reloading = true;
        WeaponControl.Reload(ref ammolocal);

    }*/

    public void KeyboardControl()
	{
		CanJump = IsGrounded();
		if (Input.GetAxis("Horizontal") != 0 && CanWalk)
		{
			transform.Translate(Input.GetAxis("Horizontal") * Vector3.right * Time.deltaTime * 1f * force);
		}

		if (Input.GetAxis("Vertical") != 0 && CanWalk)
		{
			transform.Translate(Input.GetAxis("Vertical") * Vector3.forward * Time.deltaTime * 1f * force);
		}

		if (Input.GetKeyDown(KeyCode.Space) && CanJump && Time.timeScale == 1)
		{
			gameObject.GetComponent<Rigidbody>().AddForce(new Vector2(0, 2000));
		}

		if (Input.GetKeyDown(KeyCode.I))
		{
			if (WeaponNameController.weaponname != "hand")
			{
				CursorResume = false;
			}
			if (Time.timeScale == 1||PlayerHealth<=0)
			{
				Cursor.visible = true;
				GameObject.Find("PauseMenu").GetComponent<Canvas>().enabled = true;
				pause.enabled = true;
				normalsound.enabled = false;
			}
			else if(PlayerHealth>0)
			{
				if (CursorResume)
					Cursor.visible = true; 
				else
					Cursor.visible=false;
				GameObject.Find("PauseMenu").GetComponent<Canvas>().enabled = false;
				pause.enabled = false;
				normalsound.enabled = true;
			}

		}
		if (Input.GetKeyDown(KeyCode.R) && WeaponNameController.weaponname != "hand")
		{
            Reloading = true;
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void UIControl()
	{
		timer -= Time.deltaTime;
		if (timer <= 0) 
		{
			if (timer <= -0.5) 
			{
				timer = 0.5;
			}
			if (PlayerHealth < 50) 
			{
                GameObject.Find("Heart").GetComponent<Canvas>().enabled = false;
            }
        } 
		else if (timer > 0) 
		{
            GameObject.Find("Heart").GetComponent<Canvas>().enabled = true;
        }

        if (PlayerHealth <= 0)
		{
            Cursor.visible = enabled; 
            GameObject.Find("PauseMenu").GetComponent<Canvas>().enabled = true;
            gameObject.GetComponentInChildren<Canvas>().enabled = true;
            gameObject.GetComponentInChildren<Text>().text = "You are DEAD!";
		}
	}
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public void CheckTakeItem()
	{
		if (Cantakeitem == false) {
			WaitTime -= Time.deltaTime;
			if (WaitTime < 0) 
			{
				PlayerController.Cantakeitem = true;
				WaitTime = 3;
			}
		}
	}
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void TakeDamage(int damage)
    {
        PlayerHealth -= damage;
		if (PlayerHealth > 0) 
		{
			soundEffect.PlayOneShot (liondeath, 0.7F);
		}
        //healthSlider.value = PlayerHealth;
        Debug.Log("DAMAGE! " + damage + "now player health = " + PlayerHealth);
        gameObject.GetComponentInChildren<Slider>().value = PlayerHealth;
        if (PlayerHealth == 0) 
		{
            dying = true;
            gameObject.GetComponentInChildren<Canvas>().enabled = true;
            gameObject.GetComponentInChildren<Text>().text = "You are DEAD!";
		}

    }
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [Command]
    public void CmdReload()
    {
        if (WeaponNameController.weaponname == "pistol" || WeaponNameController.weaponname == "pistol(Clone)")
        {
            ammolocal = 10;
        }
        if (WeaponNameController.weaponname == "machinegun" || WeaponNameController.weaponname == "machinegun(Clone)")
        {
            ammolocal = 30;
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [Command]
    public void CmdCheckWeapon()
    {
        Debug.Log("CheckWEapon" + WeaponNameController.weaponname);
        if (WeaponNameController.weaponname == "hand")
        {
            hit = true;
        }
        else if (WeaponNameController.weaponname == "pistol" || WeaponNameController.weaponname == "pistol(Clone)")
        {
            if (ammolocal > 0)
            {
                source.PlayOneShot(HandgunSound, 1F);
                GameObject bullet = (GameObject)Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
                flash = gameObject.GetComponentInChildren<ParticleSystem>();
                flash.Play();
                NetworkServer.Spawn(bullet);
                ammolocal--;
            }
            if (ammolocal == 0)
            {
                Debug.Log("CHECKBUG");
                mycrosshair.GetComponentInChildren<Text>().text = "RELOAD NOW!!";
                source.PlayOneShot(HandgunSoundR, 1F);
            }
        }
        else if (WeaponNameController.weaponname == "machinegun" || WeaponNameController.weaponname == "machinegun(Clone)")
        {
            if (ammolocal > 0)
            {
                source.PlayOneShot(MachinegunSound, 1F);
                Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
                muzzleFlash = GameObject.Find("Muzzle Flash m");
                flash = muzzleFlash.GetComponent<ParticleSystem>();
                flash.Play();
                ammolocal--;
            }
            if (ammolocal == 0)
            {
                source.PlayOneShot(MachinegunSoundR, 1F);
                mycrosshair.GetComponentInChildren<Text>().text = "RELOAD NOW!!";
            }
        }

    }
}
