using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] private float _speed = 5.0f;
	[SerializeField] private GameObject _laserPrefab;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(0, -3.5f, 0);
	}
	
	// Update is called once per frame
	void Update () {
		Moviment();

		if(Input.GetButtonDown("Fire1")) {
			Vector3 laserPosition = transform.position + new Vector3(0, 0.88f, 0);			
			Instantiate(_laserPrefab, laserPosition, Quaternion.identity);
		}
	}


	private void Moviment () {
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		transform.Translate(Vector3.right * _speed * horizontal * Time.deltaTime);
		transform.Translate(Vector3.up * _speed * vertical * Time.deltaTime);

		if(transform.position.x < -9.5f) {
			transform.position = new Vector3(9.5f, transform.position.y, 0);
		}

		if(transform.position.x > 9.5f) {
			transform.position = new Vector3(-9.5f, transform.position.y, 0);
		}

		if(transform.position.y > 0) {
			transform.position = new Vector3(transform.position.x, 0f, 0);
		}

		if(transform.position.y < -4.3f){
			transform.position = new Vector3(transform.position.x, -4.3f, 0);
		}
	}
}
