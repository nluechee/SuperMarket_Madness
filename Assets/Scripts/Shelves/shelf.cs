using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shelf : interactable
{
	// Interacting with a shelf
	public override void interact(){
		// Count the items collected first
		int numItemsCollected = 0;
		foreach ((string item, bool acquired) tup in gameLogic.getThis)
		{
			if (tup.acquired == true)
			{
				numItemsCollected++;
			}
		}
		if (numItemsCollected < 5)
        {
			int index = gameLogic.getThis.IndexOf((objectName, false));
			if (index != -1)
			{
				// set the item as being acquired
				gameLogic.getThis.Remove((objectName, false));
				gameLogic.getThis.Insert(index, (objectName, true));

				// Update the on-screen list
				gameLogic.displayGetText();

				// Play a correct sound here
				gameLogic.playCorrectClip();
			}
			else
			{
				// remove a second for a wrong item
				float time = gameLogic.getTimeLeft();
				gameLogic.setTimeLeft(time - 1f);

				// Play an error sound here
				gameLogic.playIncorrectClip();
			}
		}
	}
}
