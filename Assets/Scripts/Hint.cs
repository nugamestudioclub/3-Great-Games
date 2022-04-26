using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour {
	[SerializeField]
	private Text arrow;

	[SerializeField]
	private List<Text> hints;

	void Start() {
		arrow.gameObject.SetActive(false);
		foreach( var hint in hints )
			hint.gameObject.SetActive(false);
	}

	public void Reveal(GameId gameId) {
		arrow.gameObject.SetActive(true);
		hints[(int)gameId].gameObject.SetActive(true);
	}
}