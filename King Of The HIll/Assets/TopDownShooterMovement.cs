using UnityEngine;
using System.Collections;

public class TopDownShooterMovement : MonoBehaviour {

	Rigidbody rb;
	Animator anim;

	Vector3 lookPos;

	Transform cam;
	Vector3 camForward;
	Vector3 move;
	Vector3 moveInput;

	float forwardAmount;
	float turnAmount;

	public float speed = 4;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();

		lookPos = new Vector3();

		SetupAnimator();

		cam = Camera.main.transform;
	}

	void Update(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 100)) {
			lookPos = hit.point;
		}

		Vector3 lookDir = lookPos - transform.position;
		lookDir.y = 0;

		transform.LookAt (transform.position + lookDir, Vector3.up);
	}
	
	// Update is called once per frame

	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		if (cam != null) {
			camForward = Vector3.Scale (cam.up, new Vector3 (1, 0, 1)).normalized;
			move = v * camForward + h * cam.right;
		} 



		else 
		{
			move = v * Vector3.forward + h * Vector3.right;
		}






		if(move.magnitude > 1){
			move.Normalize ();
		}
			

		Move (move);

		Vector3 movement = new Vector3 (h, 0, v);

		// TODO time.deltatime not needed??
		rb.AddForce (movement * speed / Time.deltaTime);

	}

	void Move(Vector3 move){

		if(move.magnitude > 1){

			move.Normalize ();
		}

		this.moveInput = move;

		ConvertMoveInput ();
		UpdateAnimator ();
	}

	void ConvertMoveInput(){
		Vector3 localMove = transform.InverseTransformDirection (moveInput);
		turnAmount = localMove.x;

		forwardAmount = localMove.z;
	}

	void UpdateAnimator(){
		anim.SetFloat ("Forward", forwardAmount, 0.1f, Time.deltaTime);
		anim.SetFloat ("Turn", turnAmount, 0.1f, Time.deltaTime);

	}

	void SetupAnimator(){
		anim = GetComponent<Animator> ();

		/*
		foreach(Animator childAnimator in GetComponentInChildren<Animator>())
		{
			if(childAnimator != anim)
			{
				anim.avatar = childAnimator.avatar;
				Destroy (childAnimator);
				break; // if find animator stop looking
			}

		}
		*/
	}
}
