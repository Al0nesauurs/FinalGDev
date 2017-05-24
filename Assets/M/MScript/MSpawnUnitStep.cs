using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class MSpawnUnitStep : NetworkBehaviour {

    int  maxpig ;
    int maxtiger ;
    int maxboss ;
    int stage = 0;
    public GameObject myNew;
    public GameObject MachineGun;
    public GameObject Boss;
    public Transform BossSpawnlocation;
    bool spawnMachine = true;
    bool spawnBoss = true;
    bool ending = false;

    int myrandom,bossrandom;
	
	// Update is called once per frame
	void Update () {
        

        if (GameObject.FindGameObjectsWithTag("PigTag").Length==0 && 
            GameObject.FindGameObjectsWithTag("LionTag").Length == 0 &&
            GameObject.FindGameObjectsWithTag("BossTag").Length == 0&&!ending)
        {
            bossrandom = Random.Range(0, 1000);
            if (bossrandom <= 100)
            {
                var myNew = Instantiate(Boss, BossSpawnlocation.position, BossSpawnlocation.rotation);
                myNew.transform.parent = gameObject.transform;
                NetworkServer.Spawn(myNew);

                Debug.Log("myrandom = " + myrandom + " bossrandom = " + bossrandom);
                bossrandom = -1;
            }

            else if (bossrandom <= 150)
            {
                var myNew = Instantiate(Boss, BossSpawnlocation.position, BossSpawnlocation.rotation);
                myNew.transform.parent = gameObject.transform;
                NetworkServer.Spawn(myNew);
                myNew = Instantiate(Boss, BossSpawnlocation.position, BossSpawnlocation.rotation);
                myNew.transform.parent = gameObject.transform;
                NetworkServer.Spawn(myNew);

                Debug.Log("myrandom = " + myrandom + " bossrandom = " + bossrandom);
                bossrandom = -1;
            }
            else if(bossrandom==666)
            {
                var myNew = Instantiate(Boss, BossSpawnlocation.position, BossSpawnlocation.rotation);
                myNew.transform.parent = gameObject.transform;
                NetworkServer.Spawn(myNew);
                myNew = Instantiate(Boss, BossSpawnlocation.position, BossSpawnlocation.rotation);
                myNew.transform.parent = gameObject.transform;
                NetworkServer.Spawn(myNew);
                myNew = Instantiate(Boss, BossSpawnlocation.position, BossSpawnlocation.rotation);
                myNew.transform.parent = gameObject.transform;
                NetworkServer.Spawn(myNew);
                Debug.Log("myrandom = " + myrandom + " bossrandom = " + bossrandom);
                bossrandom = -1;
            }
            else
            {
                myrandom = Random.Range(0, 30);
                MPigSpawn.number = myrandom;
                MLionSpawn.number = 30 - myrandom;
                MPigSpawn.start = true;
                MLionSpawn.start = true;
                Debug.Log("myrandom = " + myrandom + " bossrandom = " + bossrandom);
            }
            
        }

    }
}
