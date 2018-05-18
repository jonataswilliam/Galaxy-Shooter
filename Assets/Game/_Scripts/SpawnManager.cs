using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	[SerializeField] private GameObject _enemyShipPrefab;
	[SerializeField] private GameObject[] _powerUpPrefab;

	private GameManager _gameManager;



	// Use this for initialization
	void Start () {
		_gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
		StartCoroutine(SpawnEnemies());
		StartCoroutine(SpawnPowerUps());
	}

	public void StartSpawnRoutines() {
		StartCoroutine(SpawnEnemies());
		StartCoroutine(SpawnPowerUps());
	}

	IEnumerator SpawnEnemies () {
		while(!_gameManager.gameOver) {
			float spawnTime = 3.0f;

			float randomX = Random.Range(-8.0f, 8.0f);
			Vector3 spawnPosition = new Vector3(randomX, 6.5f, 0f);

			Instantiate(_enemyShipPrefab, spawnPosition, Quaternion.identity);

			// if(Time.time > 60) {
			// 	spawnTime -= (Time.time / 30);
			// }

			yield return new WaitForSeconds(spawnTime);
		}
	}

	IEnumerator SpawnPowerUps() {
		while(!_gameManager.gameOver) {
			float randomX = Random.Range(-8.0f, 8.0f);
			Vector3 spawnPosition = new Vector3(randomX, 6.5f, 0f);
			int powerUpIndex = Random.Range(0, 3);
			Instantiate(_powerUpPrefab[powerUpIndex], spawnPosition, Quaternion.identity);

			yield return new WaitForSeconds(10.0f);
		}
	}
}
