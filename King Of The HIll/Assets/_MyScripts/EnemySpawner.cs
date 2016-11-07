using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public float timeToSpawn = 100; // amount of time between chances to spawn
	public float timeToSpawnTimer; // count down timer til chance to spawn
	public float percentChanceToSpawn = 100; // chance to spawn from 100
	public float xOffset = 2;
	public float yOffset = 2;

	public GameObject gameObject;


	// Use this for initialization
	void Start () {
		timeToSpawnTimer = timeToSpawn; 
	}
	
	// TODO could change to fixed update if need regular intervals
	void Update () {

		// decrement timer
		timeToSpawnTimer--;

		if (timeToSpawnTimer < 0) {
			print ("timer less than 0");
			float random = Random.Range (0, 101);
			if (random < percentChanceToSpawn) {
				print ("calling spawn");
				Spawn ();
			} else {
				print ("random not in range");
			}
			// reset timer
			timeToSpawnTimer = timeToSpawn;
		}
	}

	void Spawn(){
		Instantiate(gameObject, new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, 0), Quaternion.identity);
	}

	/*
	// spawn above
	private void CreateCoin(float x, float y){
		Instantiate(coin, new Vector3(x, y, 0), Quaternion.identity);
		CoinPickup otherScript = (CoinPickup) coin.GetComponent(typeof(CoinPickup));

	}
	*/
}
