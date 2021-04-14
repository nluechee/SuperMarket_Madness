using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class interactable : MonoBehaviour
{
	public string objectName;
	public string objectDescription;
	public string interactMessage = "Press E to collect";
    public score gameLogic;

	public float xOffset = 0;
	public float yOffset = 0;
	public float zOffset = 0;

	// Interacting with a shelf
	public abstract void interact();
}
