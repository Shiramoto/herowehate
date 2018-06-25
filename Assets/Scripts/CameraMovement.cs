using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	/// <summary>
	/// Screen Upper Limit.
	/// </summary>
	public float screenUpperLimit = 1000f;
	public float screenLowerLimit = -1000f;
	public float screenLeftLimit = -1000f;
	public float screenRightLimit = 1000f;

	public float lerpSpeed = 10;

	public Transform cameraTarget;
//	private GameObject camera;

	// Use this for initialization
	void Start () {
//		camera = GetComponent<Transform>;
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector2 tempPosition = Vector2.Lerp (transform.position, cameraTarget.position, lerpSpeed);
		transform.position.Set (tempPosition.x, tempPosition.y, transform.position.z);
	}

	bool IsWithin(Vector2 target, float up,float down,float left,float right){
		if ((target.x < up) && (target.x > down)) {
			if ((target.y < right) && (target.y > left)) {
				return true;
			}
		}
		return false;
	}

	bool IsWithin(float targetx, float targety, float up,float down,float left,float right){
		if ((targetx < up) && (targetx > down)) {
			if ((targety < right) && (targety > left)) {
				return true;
			}
		}
		return false;
	}
}
