using UnityEngine;
using System.Collections;

// from unity3d tutorial survival shooter

public class SimpePlayerMovement3D : MonoBehaviour {
	
	public float speed = 6f;            // The speed that the player will move at.

	Vector3 movement;                   // The vector to store the direction of the player's movement.
	Animator anim;                      // Reference to the animator component.
	Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
	public LayerMask floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
	float camRayLength = 100f;          // The length of the ray from the camera into the scene.

	float oldRotation = 0;

	void Awake ()
	{
		// Create a layer mask for the floor layer.
		// floorMask = LayerMask.GetMask ("Ground");

		// Set up references.
		anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent <Rigidbody> ();
	}


	void FixedUpdate ()
	{
		// Store the input axes.
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		// Move the player around the scene.
		Move (h, v);

		// Turn the player to face the mouse cursor.
		Turning ();

		// Animate based on players relationship to the mouse TODO
		AnimatingBasedOnMouse(h, v);

		// Animate the player.
		// Animating (h, v);

	
	}

	void Move (float h, float v)
	{
		// Set the movement vector based on the axis input.
		movement.Set (h, 0f, v);

		// Normalise the movement vector and make it proportional to the speed per second.
		movement = movement.normalized * speed * Time.deltaTime;

		// Move the player to it's current position plus the movement.
		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turning ()
	{
		// Create a ray from the mouse cursor on screen in the direction of the camera.
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		// Create a RaycastHit variable to store information about what was hit by the ray.
		RaycastHit floorHit;

		// Perform the raycast and if it hits something on the floor layer...
		if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			// Create a vector from the player to the point on the floor the raycast from the mouse hit.
			Vector3 playerToMouse = floorHit.point - transform.position;

			// Ensure the vector is entirely along the floor plane.
			playerToMouse.y = 0f;

			// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);

			// Set the player's rotation to this new rotation.
			playerRigidbody.MoveRotation (newRotation);

			// anim.SetFloat ("Turn", oldRotation - newRotation.y);
			// oldRotation = newRotation.y;

			anim.SetFloat ("Turn", transform.rotation.y);
		}
	}

	void Animating (float h, float v)
	{
		// Create a boolean that is true if either of the input axes is non-zero.
		// bool walking = h != 0f || v != 0f;

		// Tell the animator whether or not the player is walking.
		// anim.SetBool ("IsWalking", walking);

		// Tell animator forward speed, can be negative
		anim.SetFloat("Forward", v);
	}

	// TODO check which direction player is facing by comparing with mouse, and get speed and turning from h & v
	void AnimatingBasedOnMouse (float h, float v)
	{
		// get movement speed
		float speed = playerRigidbody.velocity.magnitude;

		anim.SetFloat ("Forward", speed);
		print ("speed: " + speed); 

		/*
		// TODO add deadzone in code or animator???

		float y = transform.rotation.y; // to shorten code

		// moving right
		if (h > 0) {
			// up right
			if (v > 0) {
				// walk
				if(y > 0){
					anim.SetFloat ("Forward", v + h);
				}
				// back petal
				else { 
					anim.SetFloat ("Forward", -(v + h));
				}
			}
			// down right
			else if (v < 0) {
				// walk
				if(y > 0){
					anim.SetFloat ("Forward", v + h);
				}
				// back petal
				else{

				}
			}

			// right
			else { // v = 0
				
			}
	    // moving left
		} else if (h < 0) {
			// up left
			if(){

			}

			// down left
			else if(){

			}

			// left
			else{

			}
	    // up and down
		} else {

		}

		*/

		// float scaledRotation = (((transform.rotation.y + 2) - 1) / (2 - 1)) -1;
		// moving right
		// print("scaled y: " + scaledRotation);

		// ** up v = 1, down v = -1, left h = -1 ,right h = 1

		// if mouse top of screen w = walk, s = back pedal

		// if mouse bottom of screen w = back pedal, s = walk



		// print ("h: " + h + ", v: " + v);
		// Create a boolean that is true if either of the input axes is non-zero.
		// bool walking = h != 0f || v != 0f;

		// Tell the animator whether or not the player is walking.
		// anim.SetBool ("IsWalking", walking);

		// Tell animator forward speed, can be negative
		// anim.SetFloat("Forward", v);

		// ** rotation.y : up = 0, down = 1, left = -0.5, right = 0.5, 
		// print ("x: " + transform.rotation.x + " , y: " + transform.rotation.y + " , z: " + transform.rotation.z);


		/*
		Vector3 localVel = transform.InverseTransformDirection(playerRigidbody.velocity);

		// print ("z: " + localVel.z + " , x: " + localVel.x);

		// walk or run
		if (localVel.z > 0.1) {
			anim.SetFloat ("Forward", v);

			animation["Walk"].speed = 0.65;
			animation.CrossFade("Walk");
			//Debug.Log("Should be walking "+localVel.z);

		}
		// back pedal
		else if (localVel.z < -0.1) {
			anim.SetFloat ("Forward", -v);
			
			animation["Backpedal"].speed = 0.65;
			animation.CrossFade("Backpedal");
			//Debug.Log("Should be backpedalling "+localVel.z);

		}
		// strafe right
		else if (localVel.x > 0.0) {
			
			animation["StrafeR"].speed = 0.65;
			animation.CrossFade("StrafeR");
			//Debug.Log("Should be strafing right "+localVel.x);

		}
		// strafe left
		else if (localVel.x < -0.0) {
			
			animation["StrafeL"].speed = 0.65;
			animation.CrossFade("StrafeL");
			//Debug.Log("Should be strafing left "+localVel.x);

		}
		// idle
		else {
			
			// animation.CrossFade("Idle");
		}
		*/

	}
}