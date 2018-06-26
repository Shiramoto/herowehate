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
    //private Transform thisCamera;
    private Vector3 initialOffset;

	// Use this for initialization
	void Start () {
		//thisCamera = GetComponent<Transform>();
        initialOffset = transform.position - cameraTarget.transform.position;
	}
	
	// Update is called once per frame
    
	void LateUpdate () {
		//cameraXY.Set (transform.position.x, transform.position.y);
		//float tempPositionX = Mathf.Lerp (transform.position.x, cameraTarget.position.x, lerpSpeed);
		//float tempPositionY = Mathf.Lerp (transform.position.y, cameraTarget.position.y, lerpSpeed);
        
		if (IsWithin (cameraTarget.position, screenUpperLimit, screenLowerLimit, screenLeftLimit, screenRightLimit)) {
            transform.position = cameraTarget.position + initialOffset;
		}
	}

	bool IsWithin(Vector3 target, float up,float down,float left,float right){
		if ((target.x < up) && (target.x > down)) {
			if ((target.y < right) && (target.y > left)) {
				return true;
			}
		}
		return false;
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
