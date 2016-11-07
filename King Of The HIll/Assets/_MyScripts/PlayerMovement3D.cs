using UnityEngine;
using System.Collections;

public class PlayerMovement3D : MonoBehaviour {

	[SerializeField] private float speed;
	[SerializeField] private float turnSpeed;
	[SerializeField] private float jumpSpeed;

	// [SerializeField] private Transform grounder; // transform for checking if ground under player
	// [SerializeField] private float radius = 0.5f;
	public LayerMask ground;

	private bool isGrounded;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rb.AddRelativeForce (Vector3.forward * speed * Input.GetAxis("Vertical"));
		rb.AddTorque (Vector3.up * turnSpeed * Input.GetAxis("Horizontal"));


		// Jumping
		// isGrounded = Physics.OverlapSphere(grounder.transform.position, radius, ground);
		// 2d version
		// isGrounded = Physics2D.OverlapCircle(grounder.transform.position, radius, ground);


		if (isGrounded & Input.GetButton ("Jump")) {
			isGrounded = false;
			rb.AddForce (Vector3.up * jumpSpeed);
		}

	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Ground") {
			isGrounded = true;
		}
	}
}
