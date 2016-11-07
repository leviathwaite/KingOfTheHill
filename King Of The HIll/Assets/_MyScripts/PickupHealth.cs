using UnityEngine;
using System.Collections;

public class PickupHealth : MonoBehaviour {

	public int healthAmount = 10;

	void OnTriggerEnter(Collider other){
			if(other.gameObject.tag == "Player"){
				// GameObject go = GameObject.Find("somegameobjectname");
				//PlayerMovement2D otherScript = (PlayerMovement2D) other.gameObject.GetComponent(typeof(PlayerMovement2D));


				// PlayerMovement2D otherScript = (PlayerMovement2D) other.GetComponent(typeof(PlayerMovement2D));
				// cause damage
				// otherScript.PickupCoin();

			PlayerHealth otherScipt = (PlayerHealth)other.GetComponent (typeof(PlayerHealth));
			otherScipt.AddHealth (healthAmount);

			Destroy (gameObject);

			}


	}
}
