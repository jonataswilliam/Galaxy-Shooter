using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] private GameObject _laserPrefab;
	[SerializeField] private GameObject _tripleshotPrefab;
	[SerializeField] private GameObject _shieldGameObject;
	[SerializeField] private GameObject[] _engineFail;
	[SerializeField] private GameObject _explosionPrefab;
	[SerializeField] private int _playerLife = 3;
	[SerializeField] private bool _hasTripleshot = false;
	[SerializeField] private bool _hasSuperSpeed = false;
	[SerializeField] private bool _hasShield = false;
	private float _speed = 5.0f;
	// Intervalo entre Disparos
	private float _fireRate = 0.25f;
	// Armazena o tempo para permitir o proximo disparo
	private float _canFire = 0.0f;
	private UIManager _uiManager;
	private GameManager _gameManager;
	private Animator _anim;
	private int engineIndex = 2;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(0, -3.5f, 0);

		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
		if(_uiManager != null) {
			_uiManager.UpdateLives(_playerLife);
		}

		_gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
		_anim = GetComponent<Animator>();
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

	public void ActiveShield () {
		_hasShield = true;
		_shieldGameObject.SetActive(true);
	}

	private void Moviment () {
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		float xMoviment = Input.GetAxisRaw("Horizontal");

		if(_hasSuperSpeed) {
			transform.Translate(Vector3.right * _speed * 1.5f * horizontal * Time.deltaTime);
			transform.Translate(Vector3.up * _speed * 1.5f * vertical * Time.deltaTime);
		} else {
			transform.Translate(Vector3.right * _speed * horizontal * Time.deltaTime);
			transform.Translate(Vector3.up * _speed * vertical * Time.deltaTime);
		}

		// Se a nave estiver indo para a esquerda
		if(xMoviment < -0) {
			_anim.SetBool("Turn_Left", true);
			_anim.SetBool("Turn_Right", false);
		// Se a nave estiver indo para a direta
		} else if (xMoviment > 0) {
			_anim.SetBool("Turn_Right", true);
			_anim.SetBool("Turn_Left", false);

		// Se não houver movimentacao lateral na nave
		} else {
			_anim.SetBool("Turn_Left", false);
			_anim.SetBool("Turn_Right", false);
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
		if(_hasShield) {
			_hasShield = false;
			_shieldGameObject.SetActive(false);
			return;
		} else {
			_playerLife--;

			if (engineIndex == 0) {
				engineIndex = 1;
			} else if (engineIndex == 1) {
				engineIndex = 0;
			} else {
				engineIndex = Random.Range(0, 2);
			}




			_engineFail[engineIndex].SetActive(true);
			_uiManager.UpdateLives(_playerLife);
		}

		if(_playerLife < 1) {
			Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
			_gameManager.gameOver = true;
			_uiManager.ShowTitleScreen();
			Destroy(this.gameObject);
		}
	}
}
