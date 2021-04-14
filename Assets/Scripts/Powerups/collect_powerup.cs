using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collect_powerup : MonoBehaviour
{
    public shopping_cart player;
    public spawn_powerups powerups;

    float powerupSpeedBoost = 6.0f;
    float timestamp = 0F;
    bool onCooldown;

    void Start(){

    }

    void Update(){

		if (timestamp > 0) {
			timestamp -= Time.deltaTime;
			if(timestamp <= 3f)         // set the player's speed back to normal after 3s
            {       
				player.setSpeed(player.getNormalSpeed());
			}
            if (timestamp <= 0 && onCooldown)       // spawn a new power up after 8s
            {
                powerups.spawnPowerup(1);
                onCooldown = false;
            }
		}
    }

    void OnTriggerEnter(Collider other){

        //If a power up is picked up
        if(other.gameObject.tag =="powerup"){
            //Destroy the object and set the timeout before another can spawn
            Destroy(other.gameObject);
            onCooldown = true;
            timestamp = 8F;
			player.setSpeed(player.getNormalSpeed() + powerupSpeedBoost);

			// Play the sound clip (powerups don't have a script that I can add a reference to game logic for)
			player.playBoostSound ();
        }
    }
}
