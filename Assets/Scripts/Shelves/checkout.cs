using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkout : interactable
{
	public timer timerScript;
	public shopping_cart cart;

	// Interacting with a shelf
	public override void interact(){
		// Count the items collected first
		int numItemsCollected = 0;
		foreach ((string item, bool acquired) tup in gameLogic.getThis)
		{
			if (tup.acquired == true) { 
				numItemsCollected++;
			}
		}

		// must have collected atleast one item so they can't just spam the checkout...
		if (numItemsCollected > 0) {

			// Add points
			gameLogic.addPoint ();

			// Remove items which are in the cart
			for (int i = gameLogic.getThis.Count - 1; i >= 0; i--) {
				if (gameLogic.getThis [i].acquired == true) {
					gameLogic.getThis.RemoveAt(i);
				}
			}

			// Deactivate the item models from the cart
			foreach (GameObject item in cart.itemModels)
            {
				item.SetActive(false);
            }

			// Set the timer 
			timerScript.setTimer(numItemsCollected);

			// Generate a new list of items to gather if the list is empty
			if (gameLogic.getThis.Count == 0) {
				gameLogic.itemRequest ();
				timerScript.setTimer(-1); // award time for completing list
				gameLogic.levelsCompleted++; // track how many lists were completed to adjust difficulty
			}

			// Update the list on screen
			gameLogic.displayGetText ();

			// Play the checkout clip
			gameLogic.playCheckoutClip();
		}
	}
}
