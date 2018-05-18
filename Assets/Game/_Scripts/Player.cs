using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


	[SerializeField] private GameObject _laserPrefab;
	[SerializeField] private GameObject _tripleshotPrefab;

	[SerializeField] private int _playerLife = 3;

	private float _speed = 5.0f;
	[SerializeField] private bool _hasTripleshot = false;
	[SerializeField] private bool _hasSuperSpeed = false;

	// Intervalo entre Disparos
	private float _fireRate = 0.25f;

	// Armazena o tempo para permitir o proximo disparo
	private float _canFire = 0.0f;


	[SerializeField] private GameObject _explosionPrefab;





	// Use this for initialization
	void Start () {
		transform.position = new Vector3(0, -3.5f, 0);
	}

	// Update is called once per frame
	void Update () {
		Moviment();
		if(Input.GetButtonDown("Fire1")) {
			shot();
		}

	}


	IEnumerator TripleShotPowerDownRoutine() {
		_hasTripleshot = true;
		yield return new WaitForSeconds(5.0f);
		_hasTripleshot = false;
	}

	IEnumerator SuperSpeedDownRoutine() {
		_hasSuperSpeed = true;
		yield return new WaitForSeconds(5.0f);
		_hasSuperSpeed = false;
	}

	public void ActiveTripleShot() {
		// 1 Forma de chamar corotinas
		StartCoroutine("TripleShotPowerDownRoutine");
	}

	public void ActiveSuperSpeed() {
		// 2 Forma de chamar corotinas
		StartCoroutine(SuperSpeedDownRoutine());
	}

	private void Moviment () {
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");


		if(_hasSuperSpeed) {
			transform.Translate(Vector3.right * _speed * 1.5f * horizontal * Time.deltaTime);
			transform.Translate(Vector3.up * _speed * 1.5f * vertical * Time.deltaTime);
		} else {
			transform.Translate(Vector3.right * _speed * horizontal * Time.deltaTime);
			transform.Translate(Vector3.up * _speed * vertical * Time.deltaTime);
		}


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

	private void shot() {
		if(Time.time > _canFire) {

			if(_hasTripleshot) {
				Instantiate(_tripleshotPrefab, transform.position + new Vector3(-0.33f, 0.06f, 0), Quaternion.identity);
			} else {
				Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
			}

			_canFire = _fireRate + Time.time;
		}
	}

	public void Damage () {
		_playerLife--;

		if(_playerLife < 1) {
			Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}
	}

}
