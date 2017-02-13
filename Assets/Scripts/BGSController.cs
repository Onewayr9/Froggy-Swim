using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSController : MonoBehaviour {

	public float scrollSpeed;

	private Renderer curRenderer;

	void Start () {
		curRenderer = GetComponent<Renderer> ();
	}

	void Update ()
	{
		Vector2 offset = new Vector2 (Time.time * scrollSpeed, 0);
		curRenderer.material.mainTextureOffset = offset;
	}
}
