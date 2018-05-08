using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] private float speed = 5.0f;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(0, -3.5f, 0);
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		transform.Translate(Vector3.right * speed * horizontal * Time.deltaTime);
		transform.Translate(Vector3.up * speed * vertical * Time.deltaTime);
	}
}
