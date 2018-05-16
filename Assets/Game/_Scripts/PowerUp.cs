using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

[SerializeField] private int _powerUpId;
// 0 - TripleShot
// 1 - Super Speed
// 2 - Shield

	private float _speed = 3.0f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.down * _speed * Time.deltaTime);

		if(transform.position.y < -5.7f) {
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other)	{
		if(other.tag == "Player") {
			Player player = other.GetComponent<Player>();
			if(player != null) {


				switch (_powerUpId) {
					case 0:
						player.ActiveTripleShot();
						break;

					case 1:
						player.ActiveSuperSpeed();
						break;

					case 2:
						// player.ActiveShield();
						break;
				}


			}

			Destroy(this.gameObject);
		}
	}
}
