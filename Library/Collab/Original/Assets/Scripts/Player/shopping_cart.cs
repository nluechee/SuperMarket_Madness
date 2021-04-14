using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopping_cart : MonoBehaviour
{
    private bool checkInput = false;
    private Collider shelf_collider;
	public score scoreScript;

	// Animation
	public Animator animator;
	public GameObject items1, items2, items3, items4, items5;
	public List<GameObject> itemModels = new List<GameObject>();
	int itemCount;

	// Movement
	Rigidbody rb;
	float verticalIn, horizontalIn;
	float minThresh = 0.1F;
	float normalSpeed = 16.0F;
	float speed;						// Movement speed
	Vector3 lastPosition = Vector3.forward;		// Last postion of the player
	Vector3 dirVec = new Vector3(0,0,0);			// Movement direction of the player
	bool isMenuOpen = false;

	// Object interaction
	float detectionRadius = 3.0F;
	KeyCode interactKey = KeyCode.E;
	string shaderPath = ("highlight");
	bool isActive;
	bool isDisplayed;
	GameObject closestObject;
	Collider closestCollider;
	interactable interactObject;
	public GameObject oopsText;
	float enemyCooldown;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
		lastPosition = transform.localPosition;
		speed = normalSpeed;
		itemModels.Add(items1);
		itemModels.Add(items2);
		itemModels.Add(items3);
		itemModels.Add(items4);
		itemModels.Add(items5);
		enemyCooldown = 0f;
	}

    // Update is called once per frame
    void Update()
    {
		/*---------------------------------------------------------------------*\
		|							Interactable Objects						|
		\* --------------------------------------------------------------------*/

		// Find nearby colliders of objects in the "Interactable" layer
		Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, detectionRadius, 1<<LayerMask.NameToLayer("Interactable"));

		// If there is no nearest object, make sure these are set to false (e.g. they were destroyed by another script)
		if (closestObject == null) isActive = false;

		// If near an object, set the shader
		if(nearbyColliders.Length != 0 && !isActive){
			// Get the object, set the shader
			closestObject = nearbyColliders[0].gameObject;
			closestCollider = nearbyColliders [0];
			closestObject.GetComponent<Renderer>().material.shader = Shader.Find(shaderPath);

			// Display the info
			interactObject = closestObject.GetComponent<interactable>();
			isDisplayed = true;

			isActive = true;
		}
		// Else return it to default
		else if(nearbyColliders.Length == 0 && closestObject != null && isActive)
		{  
			closestObject.GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
			isDisplayed = false;
			isActive = false;
			closestObject = null;
			closestCollider = null;
		}
		// If key press, interact with it
		if(isActive && Input.GetKeyDown(interactKey))
		{
			interactObject = closestObject.GetComponent<interactable>();
			interactObject.interact();
		}

		// Animation when moving
		if (Mathf.Abs(Input.GetAxis("Vertical")) > 0 || Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
		{
			animator.SetBool("moving", true);
		}
		else
		{
			animator.SetBool("moving", false);
		}

		//	count the cooldown before an enemy can hit the player again
		if (enemyCooldown > 0)
        {
			enemyCooldown -= Time.deltaTime;
		}
	}

    private void FixedUpdate()
    {

		/*---------------------------------------------------------------------*\
		|							Player Movement								|
		\* --------------------------------------------------------------------*/

		// Get input
		verticalIn = (Mathf.Abs(Input.GetAxis("Vertical")) > minThresh ? Input.GetAxis("Vertical") : 0);
		horizontalIn = (Mathf.Abs(Input.GetAxis("Horizontal")) > minThresh ? Input.GetAxis("Horizontal") : 0);
		

		if (!isMenuOpen) {
			//rb.velocity = ((verticalIn * Vector3.forward + horizontalIn * Vector3.right).normalized * speed);

			Vector3 targetVelocity = ((verticalIn * Vector3.forward + horizontalIn * Vector3.right).normalized * (speed + getSpeedMod()));
			Vector3 force = (targetVelocity - rb.velocity) * 30;
			rb.AddForce(force);
		}

		// If the player falls through the plane, bring them back up
		if (transform.position.y < -1)
			transform.position = new Vector3 (transform.position.x, 1, transform.position.z);

		// Manage rotation
		if(transform.localPosition != lastPosition){
			dirVec = Vector3.ProjectOnPlane ((transform.localPosition - lastPosition), Vector3.up);
			if(dirVec.magnitude > 0.2) lastPosition = transform.localPosition;
			if(dirVec.magnitude > 0.1) transform.rotation = Quaternion.LookRotation(dirVec, Vector3.up);
		}
	}
		
    private void OnTriggerStay(Collider trigger)
    {
        shelf_collider = trigger;
        checkInput = true;

		Debug.Log (shelf_collider.gameObject.tag);
		if (shelf_collider.gameObject.tag == "Enemy") {
			if (itemCount > 0 && enemyCooldown<=0){
				Instantiate(oopsText, transform.position, Quaternion.identity);
				enemyCooldown = 2f;
				scoreScript.hitEnemy();
				itemModels[itemCount - 1].SetActive(false);
				itemCount--;
				for (int i = 0; i < itemCount; i++)
				{
					itemModels[i].SetActive(true);
				}
			}
		}
    }
    private void OnTriggerExit(Collider trigger)
    {
        checkInput = false;
    }

    public bool getCheckInput()
    {
        return checkInput;
    }

    public Collider getCollider()
    {
        return shelf_collider;
    }

	public void setMenuOpen(bool isOpen){
		isMenuOpen = isOpen;
	}

	public void setSpeed(float newSpeed){
		speed = newSpeed;
	}

	public float getSpeed(){
		return speed;
	}

	public float getNormalSpeed(){
		return normalSpeed;
	}

	private float getSpeedMod(){
		itemCount = 0;
		foreach ((string item, bool acquired) tup in scoreScript.getThis)
		{
			if (tup.acquired == true && itemCount<6)
			{
				itemCount++;
			}
		}
		// display the models for the amount of items in the cart
		for (int i = 0; i < itemCount; i++)
		{
			itemModels[i].SetActive(true);
		}

		switch (itemCount){
			case 1:
				return -2.0f;
			case 2:
				return -4.0f;
			case 3:
				return -7.0f;
			case 4:
				return -8.0f;
			case 5:
				return -9.0f;
			default:
				return 0;
		}
	}


    /*---------------------------------------------------------------------*\
	|									GUI									|
	\* --------------------------------------------------------------------*/

    void OnGUI(){
		GUI.contentColor = Color.black;
		GUI.skin.label.fontSize = 30;
		if(isDisplayed && closestObject != null){
			Vector3 objectScreenPosition = Camera.main.WorldToScreenPoint (closestCollider.transform.position + new Vector3 (interactObject.xOffset, interactObject.yOffset, interactObject.zOffset));
			GUI.Label(new Rect(objectScreenPosition.x, Screen.height - objectScreenPosition.y, 300, 100), interactObject.interactMessage + " " + interactObject.objectName + "\r\n<i>" + interactObject.objectDescription + "</i>");
		}
	}
}