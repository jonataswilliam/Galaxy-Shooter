using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private float _speed = 3.0f;
	[SerializeField] private GameObject EnemyExplosionPrefab;
	[SerializeField] private AudioClip _explosionAudio;

	private UIManager _uiManager;
	private GameManager _gameManager;

	// Use this for initialization
	void Start () {
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
		_gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
	}

	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.down * _speed * Time.deltaTime);
		if(transform.position.y < -7.0f) {

			// Verifica se o jogo esta ativo ou esta em gameOver antes de repor os inimigos na tela
			if(!_gameManager.gameOver) {
				// Redefine um valor aleatorio em X para respawn do inimigo
				float randomX = Random.Range(-8.0f, 8.0f);
				transform.position = new Vector3(randomX, 7.0f, 0);
			} else {
				Destroy(this.gameObject);
			}

		}
	}

	void OnTriggerEnter2D(Collider2D other)	{
		if (other.tag == "Player") {
			Player player = other.GetComponent<Player>();
			player.Damage();

			_uiManager.UpdateScore(100);

			Instantiate(EnemyExplosionPrefab, transform.position, Quaternion.identity);

			AudioSource.PlayClipAtPoint(_explosionAudio, Camera.main.transform.position);
			Destroy(this.gameObject);

		} else if(other.tag == "PlayerLaser") {
			if(other.transform.parent != null) {
				Destroy(other.transform.parent.gameObject);
			}

			_uiManager.UpdateScore(100);
			AudioSource.PlayClipAtPoint(_explosionAudio, Camera.main.transform.position);
			Instantiate(EnemyExplosionPrefab, transform.position, Quaternion.identity);
			Destroy(other.gameObject);
			Destroy(this.gameObject);
		}
	}
}
