using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour {
	private Rigidbody rigidBody;
	public float drag;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		rigidBody.drag = drag;
	}
	
	// Update is called once per frame
//	void Update () {
//	
//	}
}
