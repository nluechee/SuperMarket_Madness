using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShelfMenu : MonoBehaviour
{

    public Collider col;
    bool checkInput;
    public shopping_cart playerScript;
    public GameObject coldShelf;
    public GameObject meatShelf;
    public GameObject vegShelf;
    public GameObject snackShelf;
    public GameObject bakeryShelf;
	public bool checkoutObj;


	private GameObject shelfObj;


    // Update is called once per frame
    void Update()
    {
        checkInput = playerScript.getCheckInput();
        // open shelves
        if (checkInput == true)
        {
            col = playerScript.getCollider();
			if (Input.GetKeyDown (KeyCode.E)) {
				checkoutObj = false;
				// Get the shelf object
				switch (col.gameObject.tag) {
				case "cold":
					shelfObj = coldShelf;
					break;
				case "meat":
					shelfObj = meatShelf;
					break;
				case "veggies":
					shelfObj = vegShelf;
					break;
				case "snacks":
					shelfObj = snackShelf;
					break;
				case "bakery":
					shelfObj = bakeryShelf;
					break;
				case "checkout":
					checkoutObj = true;
					break;
				}
				if(checkoutObj == false){
					// Toggle its menu and player movement
					shelfObj.SetActive (!shelfObj.activeSelf);
					playerScript.setMenuOpen(shelfObj.activeSelf);
				}
			}
        }
    }

}
