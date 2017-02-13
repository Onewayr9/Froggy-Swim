using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float jumpheight = 30f;

	private Rigidbody rbd;
	// Use this for initialization
	void Start () {
		rbd = GetComponent<Rigidbody> ();
	}

	void OnCollisionEnter(Collision collision) {
		if (!GameManager.instance.isGround && collision.collider.tag == "Ground") {
			GameManager.instance.isGround = true;
		}
	}

	// Update is called once per frame
	void Update () {
		print (GameManager.instance.isGround);
		if (GameManager.instance.isGround && Input.GetKeyDown ("space")) {
			rbd.AddForce (new Vector3 (0, 350, 0));
			GameManager.instance.isGround = false;
		}
	}
}
