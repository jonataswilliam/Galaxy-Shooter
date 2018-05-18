using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	[SerializeField] private GameObject _enemyShipPrefab;
	[SerializeField] private GameObject[] _powerUpPrefab;

	// Use this for initialization
	void Start () {
		StartCoroutine(SpawnEnemies());
		StartCoroutine(SpawnPowerUps());
	}

	IEnumerator SpawnEnemies () {
		while(true) {
			float randomX = Random.Range(-8.0f, 8.0f);
			Vector3 spawnPosition = new Vector3(randomX, 6.5f, 0f);

			Instantiate(_enemyShipPrefab, spawnPosition, Quaternion.identity);
			yield return new WaitForSeconds(5.0f);
		}
	}

	IEnumerator SpawnPowerUps() {
		while(true) {
			float randomX = Random.Range(-8.0f, 8.0f);
			Vector3 spawnPosition = new Vector3(randomX, 6.5f, 0f);			
			int powerUpIndex = Random.Range(0, 3);
			Instantiate(_powerUpPrefab[powerUpIndex], spawnPosition, Quaternion.identity);

			yield return new WaitForSeconds(10.0f);
		}
	}
}
