using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public int startingHealth = 100;            // The amount of health the enemy starts the game with.
	public int currentHealth;                   // The current health the enemy has.
	public float sinkSpeed = 2.5f;              // The speed at which the enemy sinks through the floor when dead.
	public int scoreValue = 10;                 // The amount added to the player's score when the enemy dies.
	public AudioClip deathClip;                 // The sound to play when the enemy dies.


	Animator anim;                              // Reference to the animator.
	AudioSource enemyAudio;                     // Reference to the audio source.
	public ParticleSystem hitParticles;                // Reference to the particle system that plays when the enemy is damaged.
	CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
	bool isDead;                                // Whether the enemy is dead.
	bool isSinking;                             // Whether the enemy has started sinking through the floor.
	public bool isRecentlyHurt = false;                        // for tracking recently hurt, allowing blinking
	float recentlyHurtTime = 60.0f;
	public float recentlyHurtTimer;

	private Rigidbody rb;
	public float force = 100; // force to push back TODO pass from weapon
	public float radius = 1; // radius of push back explosion


	void Awake ()
	{
		recentlyHurtTimer = recentlyHurtTime;

		// Setting up the references.
		anim = GetComponent <Animator> ();
		enemyAudio = GetComponent <AudioSource> ();
		//hitParticles = GetComponentInChildren <ParticleSystem> ();
		capsuleCollider = GetComponent <CapsuleCollider> ();

		// Setting the current health when the enemy first spawns.
		currentHealth = startingHealth;

		rb = GetComponent<Rigidbody> ();
	}

	void Update ()
	{
		if(isRecentlyHurt){
			recentlyHurtTimer--;
		}

		if (recentlyHurtTimer < 0.0f) {
			isRecentlyHurt = false;
			recentlyHurtTimer = recentlyHurtTime;
		}

		// If the enemy should be sinking...
		if(isSinking)
		{
			// ... move the enemy down by the sinkSpeed per second.
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
		}
	}


	public void TakeDamage (int amount, Vector3 hitPoint)
	{
		// set recentlyHurt to true and set timer
		isRecentlyHurt = true;
		recentlyHurtTimer = recentlyHurtTime;

		// If the enemy is dead...
		if(isDead)
			// ... no need to take damage so exit the function.
			return;

		isRecentlyHurt = true;
		recentlyHurtTimer = recentlyHurtTime;

		// Play the hurt sound effect.
		enemyAudio.Play ();

		// Reduce the current health by the amount of damage sustained.
		currentHealth -= amount;

		// push back from hit
		rb.AddExplosionForce(force, hitPoint, radius, 0.0F); // 0.0f forcemode

		/*
			Force	Add a continuous force to the rigidbody, using its mass.
			Acceleration	Add a continuous acceleration to the rigidbody, ignoring its mass.
			Impulse	Add an instant force impulse to the rigidbody, using its mass.
			VelocityChange	Add an instant velocity change to the rigidbody, ignoring its mass.
		*/

		// Set the position of the particle system to where the hit was sustained.
		hitParticles.transform.position = hitPoint;

		// And play the particles.
		hitParticles.Play();

		// If the current health is less than or equal to zero...
		if(currentHealth <= 0)
		{
			// ... the enemy is dead.
			Death ();
		}
	}


	void Death ()
	{
		// The enemy is dead.
		isDead = true;
		StartSinking ();

		// Turn the collider into a trigger so shots can pass through it.
		capsuleCollider.isTrigger = true;

		// Tell the animator that the enemy is dead.
		anim.SetTrigger ("Dead");

		// Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
		enemyAudio.clip = deathClip;
		enemyAudio.Play ();
	}


	public void StartSinking ()
	{
		// Find and disable the Nav Mesh Agent.
		GetComponent <NavMeshAgent> ().enabled = false;

		// Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
		GetComponent <Rigidbody> ().isKinematic = true;

		// The enemy should no sink.
		isSinking = true;

		// Increase the score by the enemy's score value.
		// ScoreManager.score += scoreValue;

		// TODO reset and reuse, not destroy
		// After 2 seconds destory the enemy.
		Destroy (gameObject, 2f);
	}
}