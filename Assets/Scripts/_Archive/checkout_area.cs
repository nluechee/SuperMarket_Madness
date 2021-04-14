using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkout_area : MonoBehaviour
{
    public Collider col;
	public shopping_cart playerScript;

    private bool checkOut;
    private bool checkInput;


    // Start is called before the first frame update
    void Start()
    {
        checkOut = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        checkInput = playerScript.getCheckInput();
        // open shelves
        if (checkInput == true)
        {
            /*col = playerScript.getCollider();
			if (Input.GetKeyDown (KeyCode.E)) {
                if(col.gameObject.tag == "checkout"){
                    checkOut = true;
                }
            }*/
        }
    }

    public bool getCheckout(){
        return checkOut;
    }

    public void setCheckout(bool checkoutVal){
        checkOut = checkoutVal;
    }
}