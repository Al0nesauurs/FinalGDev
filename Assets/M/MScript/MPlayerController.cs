using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class MPlayerController : NetworkBehaviour
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
    public MWeaponController WeaponControl;
    public MWeaponNameController WeaponNameControl;
    public Camera Tps;
    public Camera Fps;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
	private double timer = 1.0;
	public AudioSource pause;
	public AudioSource normalsound;
	AudioSource soundEffect;
	public AudioClip Splayerhurt;
    public AudioClip Splayerdeath;
	public static bool Cantakeitem = true;
    public int ammolocal=0;
    public GameObject HpUI;


    //port from WEAPONCONTROLLER.CS
    //sound part
    public AudioClip HandgunSound;
    public AudioClip MachinegunSound;
    public AudioClip HandgunSoundR;
    public AudioClip MachinegunSoundR;
    public AudioClip mysound;
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
    public GameObject crosshair;
    public GameObject mySlider;
    public static int healthbar = 100;
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
            }
            healthbar = PlayerHealth;
            UIControl();
        }

    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void CheckHaveGun()
    {
        if (HaveGun == false)
        {
            //HaveGun = true;
            MWeaponNameController.weaponname = "Mpistol";
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
			if (MWeaponNameController.weaponname != "hand")
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
		if (Input.GetKeyDown(KeyCode.R) && MWeaponNameController.weaponname != "hand")
		{
            Reloading = true;
            ammolocal = 0;
            crosshair.GetComponent<MCrosshairManager>().Reload();
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

		}
	}
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public void CheckTakeItem()
	{
		if (Cantakeitem == false) {
			WaitTime -= Time.deltaTime;
			if (WaitTime < 0) 
			{
				MPlayerController.Cantakeitem = true;
				WaitTime = 3;
			}
		}
	}
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void TakeDamage(int damage)
    {
        PlayerHealth -= damage;
        healthbar = PlayerHealth;
		if (PlayerHealth > 0) 
		{
            mysound = Splayerhurt;
            RpcPlaysound(1.5f);
		}
        else if(PlayerHealth==0)
        {
            mysound = Splayerdeath;
            RpcPlaysound(1.5f);
        }
        //healthSlider.value = PlayerHealth;
        gameObject.GetComponentInChildren<Slider>().value = PlayerHealth;
        Debug.Log("DAMAGE! " + damage + "now player health = " + PlayerHealth);
        if (PlayerHealth == 0) 
		{
            gameObject.transform.rotation = Quaternion.Euler(-90, gameObject.transform.rotation.y, 0);
            gameObject.GetComponentInChildren<Canvas>().enabled = true;
            dying = true;
            crosshair.GetComponent<MCrosshairManager>().DEATH();
            gameObject.tag = "Death";
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
        if (MWeaponNameController.weaponname == "Mpistol" || MWeaponNameController.weaponname == "Mpistol(Clone)")
        {
            ammolocal = 10;
        }
        if (MWeaponNameController.weaponname == "Mmachinegun" || MWeaponNameController.weaponname == "Mmachinegun(Clone)")
        {
            ammolocal = 30;
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
   [Command]
    public void CmdCheckWeapon()
    {
        Debug.Log("CheckWEapon" + MWeaponNameController.weaponname);
        if (MWeaponNameController.weaponname == "hand")
        {
            hit = true;
        }
        else if (MWeaponNameController.weaponname == "Mpistol" || MWeaponNameController.weaponname == "Mpistol(Clone)")
        {
            if (ammolocal > 0)
            {
                GameObject bullet = Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
                RpcFlash();
                NetworkServer.Spawn(bullet);
                ammolocal--;
            }
            else 
            {
                RpcNoammo();
                mysound = HandgunSoundR;
                RpcPlaysound(1F);

            }

        }
        else if (MWeaponNameController.weaponname == "Mmachinegun" || MWeaponNameController.weaponname == "Mmachinegun(Clone)")
        {
            if (ammolocal > 0)
            {
                GameObject bullet = Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
                RpcFlash();
                NetworkServer.Spawn(bullet);
                ammolocal--;
            }
            if (ammolocal == 0)
            {
                mysound = MachinegunSound;
                RpcPlaysound(1F);
                RpcNoammo();
            }
        }

    }
    [ClientRpc]
    void RpcFlash()
    {
        flash = gameObject.GetComponentInChildren<ParticleSystem>();
        flash.Play();
        if (MWeaponNameController.weaponname == "Mpistol" || MWeaponNameController.weaponname == "Mpistol(Clone)")
        {
            mysound = HandgunSound;
            RpcPlaysound(1F);
        }
        else if (MWeaponNameController.weaponname == "Mmachinegun" || MWeaponNameController.weaponname == "Mmachinegun(Clone)")
        {
            mysound = MachinegunSound;
            RpcPlaysound(1F);
        }
    }
    [ClientRpc]
    void RpcNoammo()
    {
        crosshair.GetComponent<MCrosshairManager>().Noammo();
    }
    [ClientRpc]
    void RpcPlaysound(float vol)
    {
        source.PlayOneShot(mysound, vol);
    }
}
