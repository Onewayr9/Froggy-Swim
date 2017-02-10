using UnityEngine;
using System.Collections;

public class RotateAroundY : MonoBehaviour {
	public Vector3 rotateVector;
	private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		rigidBody.angularVelocity = rotateVector;
	}

}
