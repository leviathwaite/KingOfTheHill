using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResetOnFall : MonoBehaviour {

	[SerializeField] private float yTrigger = 0; // at what height is scene reset

	void FixedUpdate () {
		if (transform.position.y < yTrigger) {
			Reload ();
		}
	}

	public void Reload()
	{
		int scene = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(scene, LoadSceneMode.Single);
		Time.timeScale = 1;
	}
}
