using UnityEngine;
using System.Collections;

public class CompassWalker : MonoBehaviour {
	/*
	[System.Serializable]
	public class Direction {
		public string name;
		public SkeletonDataAsset data;
		public bool flipX;
		public SkeletonAnimation animation;
	}

	public SkeletonDataAsset animationSource;
	[SpineAnimation(dataField: "animationSource")]
	public string idleAnim;

	[SpineAnimation(dataField: "animationSource")]
	public string walkAnim;

	

	public Direction[] directions;
	public bool useFlipTimeCompensation = true;
	public float speed = 2;

	Direction currentDirection;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < directions.Length; i++) {
			SpawnDirection(directions[i]);
		}

		SetDirection(0);
		SetAnimation(idleAnim);
	}

	void SpawnDirection (Direction dir) {
		if (dir.animation == null) {
			GameObject go = new GameObject(dir.name, typeof(SkeletonAnimation));
			dir.animation = go.GetComponent<SkeletonAnimation>();
			dir.animation.skeletonDataAsset = dir.data;
			go.transform.parent = transform;
			go.renderer.enabled = false;

			dir.animation.Reset();
		} else {
			dir.animation.gameObject.GetComponent<Renderer>().enabled = false;
			dir.animation.Reset();
		}
		
	}

	void SetDirection (int i) {
		if (currentDirection == directions[i])
			return;

		float time = 0;
		string animName = "";
		bool oldFlip = false;
		if (currentDirection != null) {
			oldFlip = currentDirection.flipX;
			var oldTrack = currentDirection.animation.state.GetCurrent(0);
			animName = oldTrack.Animation.Name;
			time = oldTrack.Time % oldTrack.Animation.Duration;
			currentDirection.animation.enabled = false;
			currentDirection.animation.renderer.enabled = false;
		}

		currentDirection = directions[i];
		var skeleton = currentDirection.animation.Skeleton;

		skeleton.FlipX = currentDirection.flipX;

		currentDirection.animation.enabled = true;
		currentDirection.animation.renderer.enabled = true;

		if (animName != "") {
			var state = currentDirection.animation.state;
			var track = state.SetAnimation(0, animName, true);

			if (useFlipTimeCompensation) {
				if (currentDirection.flipX != oldFlip) {
					time = track.Animation.Duration - time;
				}
			}

			track.LastTime = 0;
			track.Time = time;
			track.Animation.Apply(skeleton, 0, time, true, null);
			skeleton.UpdateWorldTransform();
		}
	}

	void SetAnimation (string anim) {
		var track = currentDirection.animation.state.GetCurrent(0);
		if (track != null) {
			if (track.Animation.Name == anim)
				return;
		}

		currentDirection.animation.state.SetAnimation(0, anim, true);
		
	}

	void Update () {

		Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

		if (dir.magnitude > 0.1f) {
			float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
			if (angle < 0)
				angle += 360;

			int dirIndex = 0;

			if (angle <= 22.5f || angle > 337.5f)
				dirIndex = 0;
			else if (angle > 22.5f && angle <= 67.5f)
				dirIndex = 1;
			else if (angle > 67.5f && angle <= 112.5f)
				dirIndex = 2;
			else if (angle > 112.5f && angle <= 157.5f)
				dirIndex = 3;
			else if (angle > 157.5f && angle <= 202.5f)
				dirIndex = 4;
			else if (angle > 202.5f && angle <= 247.5f)
				dirIndex = 5;
			else if (angle > 247.5f && angle <= 292.5f)
				dirIndex = 6;
			else
				dirIndex = 7;

			Vector3 moveVec = Quaternion.Euler(0, dirIndex * 45, 0) * Vector3.forward * speed * Time.deltaTime;

			transform.Translate(moveVec);

			SetAnimation(walkAnim);
			SetDirection(dirIndex);
		} else {
			SetAnimation(idleAnim);
		}
	}
	*/
}
