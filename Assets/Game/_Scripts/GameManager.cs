using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public bool gameOver = true;
	public GameObject player;
	private UIManager _uiManager;
	private SpawnManager _spawnManager;

	// if GameOver = true
		// if spaceKey pressed
			// spawn the player
			// gameOver is false
			// hide title screen

	void Start() {
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
		_spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
	}



	void Update() {
		if(gameOver) {
			if(Input.GetKeyDown(KeyCode.Space)) {
				Instantiate(player, new Vector3(0, -4, 0), Quaternion.identity);
				gameOver = false;
				_uiManager.HideTitleScreen();
				_spawnManager.StartSpawnRoutines();

			}
		}
	}

}
