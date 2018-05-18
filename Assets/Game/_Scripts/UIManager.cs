using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Biblioteca para uso de componentes da UI
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Sprite[] lives;
	public Image ImageHUDLives;

	public void UpdateLives(int currentLives) {
		// Acessando o sprite do componente e alterando para a outra imagem cadastrado no array.
		ImageHUDLives.sprite = lives[currentLives];
	}

	public void UpdateScore() {

	}
}
