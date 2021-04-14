using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    Spawn powerups
**/
public class spawn_powerups : MonoBehaviour
{
    public GameObject powerup;
    public GameObject spawn1, spawn2, spawn3, spawn4;
    private List<GameObject> spawns = new List<GameObject>();
    private Vector3 spawnLocation, offset;
    int spawnID, last;

    private int powerupCount;
    private int maxPowerups = 1;
    void Start()
    {
        last = -1;
        spawns.Add(spawn1);
        spawns.Add(spawn2);
        spawns.Add(spawn3);
        spawns.Add(spawn4);
        powerupCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (powerupCount < maxPowerups){

            // pick one of the 4 spawn locations
            spawnID = Random.Range(0, 4);
            // prevent spawning in the same place
            while (spawnID == last)
            {
                spawnID = Random.Range(0, 4);
            }
            spawnLocation = spawns[spawnID].transform.position;

            //Create new powerup object and set its position
            GameObject newPowerup = GameObject.Instantiate(powerup);
            offset = new Vector3(0, 1, 0);
            newPowerup.transform.position = spawnLocation + offset;
            last = spawnID;
            
            //Keep track of count
            powerupCount = powerupCount +1;
        }
     }
		
    public void spawnPowerup(int count){
        powerupCount = powerupCount - count;
    }
}

