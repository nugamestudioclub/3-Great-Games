using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour {
	[SerializeField]
	private GameObject arrow;

	[SerializeField]
	private List<GameObject> hints;

	void Start() {
		arrow.SetActive(false);
		foreach( var hint in hints )
			hint.SetActive(false);
	}

	public void Reveal(GameId gameId) {
		arrow.SetActive(true);
		hints[(int)gameId].SetActive(true);
	}
}