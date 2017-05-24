using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MPigController : NetworkBehaviour
{
    Transform playerarm;
    float trun = 0;
    [SyncVar]  public float hp = 12;
    public static float damageApply = 0;
    bool running = false;
    bool fliping = false;
    float TimetoWalk = -10;
    public float speedup = 0;
    public GameObject meat;
    public float pigspeed = 0.01f;
	public AudioClip hitpig;
	public AudioClip pigdeath;
	AudioSource audio;

    // Use this for initialization
    void Start()
    {
		audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer)
        {
            return;
        }
        playerarm = GameObject.Find("PlayerArm").GetComponent<Transform>();
        if (running)
        {
            run(trun += Time.deltaTime);
        }
        else
        {
            if (TimetoWalk <= 0)
            {
                if (!fliping)
                {
                    Vector3 wayPoint = new Vector3(Random.Range(-100, 100), gameObject.transform.position.y, Random.Range(-100, 100));
                    gameObject.transform.LookAt(wayPoint);
                    fliping = true;
                }


                gameObject.transform.Translate(Vector3.forward * pigspeed * Time.deltaTime*100);
                TimetoWalk -= Time.deltaTime;
                if (TimetoWalk <= -5)
                {
                    fliping = false;
                    TimetoWalk = Random.Range(10, 15);
                }
            }
            else
            {
                TimetoWalk -= Time.deltaTime + speedup;
            }
        }

    }

    public void HpController(int damage)
    {
        Debug.Log("HP CON!");
        running = true;
        fliping = true;
        gameObject.transform.Translate(Vector3.up * Time.deltaTime * 10f);
        hp -= damage;
		if (hp >= 1) 
		{
			audio.PlayOneShot(hitpig, 0.7F);
		}

        if (hp <= 0)
        {
			AudioSource.PlayClipAtPoint(pigdeath, transform.position);
            DropItem();
            DropItem();
            DropItem();
            MScoreManager.Score += 5;
            Destroy(gameObject);

        }
    }
    void run(float timerun)
    {

        if (timerun >= 3)
        {
            running = false;
            trun = 0;
        }
        var targetPosition = playerarm.position;
        targetPosition.y = transform.position.y;
        gameObject.transform.LookAt(targetPosition);
        gameObject.transform.Rotate(0, 180, 0);
        gameObject.transform.Translate(Vector3.forward * pigspeed * 2);

    }
    void DropItem()
    {
        Vector3 meatposition = new Vector3(Random.Range(gameObject.transform.position.x + 0.3f, gameObject.transform.position.x - 0.3f), gameObject.transform.position.y, gameObject.transform.position.z);
        GameObject Dmeat = (GameObject) Instantiate(meat, meatposition, Quaternion.identity);
        NetworkServer.Spawn(Dmeat);
    }

}
