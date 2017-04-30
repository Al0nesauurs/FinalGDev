using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class SpawnUnitStep : NetworkBehaviour {

    int [] maxpig =   { 10,7, 6, 5, 2, 0, 0};
    int [] maxtiger = { 0, 2, 3, 5, 7, 0, 0 };
    int[] maxboss =   { 0, 0, 0, 0, 0, 1, 0 };
    int stage = 0;
    public GameObject myNew;
    public GameObject MachineGun;
    public GameObject Boss;
    public Transform BossSpawnlocation;
    bool spawnMachine = true;
    bool spawnBoss = true;
    bool ending = false;

	
	// Update is called once per frame
	void Update () {
        

        if (GameObject.FindGameObjectsWithTag("PigTag").Length==0 && 
            GameObject.FindGameObjectsWithTag("LionTag").Length == 0 &&
            GameObject.FindGameObjectsWithTag("BossTag").Length == 0&&!ending)
        {
            Debug.Log("stage= " + stage);
            PigSpawn.number = maxpig[stage];
            LionSpawn.number = maxtiger[stage];
            PigSpawn.start = true;
            LionSpawn.start = true;
            stage++;
        }

        if (stage == 3 && spawnMachine)
        {
            myNew = Instantiate(MachineGun, gameObject.transform.position, gameObject.transform.rotation);
            spawnMachine = false;
        }
        if (stage==6&&spawnBoss)
        {
            Instantiate(Boss, BossSpawnlocation.position, BossSpawnlocation.rotation);
            spawnBoss = false;
        }

        if(stage==7)
        {
            ending = true;
            Time.timeScale = 0;
            GameObject.Find("Crosshair").GetComponent<Text>().text = "Thank for playing !!!!";
        }
    }
}
