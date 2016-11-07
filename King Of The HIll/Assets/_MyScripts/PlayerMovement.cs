using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed = 5;
	public float turnSpeed = 2;
	public float jumpHieght = 2;

	public float gravity = 0.8f;



	// Use this for initialization
	void Start () {
	
	}
	

	void FixedUpdate () {
		transform.Translate (0, 0, Input.GetAxis("Vertical") * speed);


		if (Input.GetButton ("Jump")) {
			transform.Translate (0, jumpHieght, 0);
		}

		if (transform.position.y >= 0) {
			transform.Translate (0, -gravity, 0);
			if(transform.position.y < 0){
				transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
			}

		}

		transform.Rotate (0, Input.GetAxis("Horizontal") * turnSpeed, 0);
	}
}
