using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour {

	[SerializeField] private AudioClip _explosionSound;
	// Use this for initialization
	void Start () {
		AudioSource.PlayClipAtPoint(_explosionSound, Camera.main.transform.position);
		Destroy(this.gameObject, 3f);
	}
}

