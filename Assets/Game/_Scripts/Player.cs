using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] private float _speed = 5.0f;
	[SerializeField] private GameObject _laserPrefab;
	[SerializeField] private GameObject _tripleShootPrefab;

	[SerializeField] bool hasTripleShoot = false;



	// Intervalo entre Disparos
	private float _fireRate = 0.25f;

	// Armazena o tempo para permitir o proximo disparo
	private float _canFire = 0.0f;



	// Use this for initialization
	void Start () {
		transform.position = new Vector3(0, -3.5f, 0);
	}

	// Update is called once per frame
	void Update () {
		Moviment();
		if(Input.GetButtonDown("Fire1")) {
			Shoot();
		}

	}

	private void Shoot() {
		if(Time.time > _canFire) {

			if(hasTripleShoot) {
				Instantiate(_tripleShootPrefab, transform.position + new Vector3(-0.33f, 0.06f, 0), Quaternion.identity);
			} else {
				Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
			}

			_canFire = _fireRate + Time.time;
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
