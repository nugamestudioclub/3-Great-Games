using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MemoryLabel : MonoBehaviour {
	private Text text;

	[SerializeField]
	GameId gameId;

	void Awake() {
		text = GetComponent<Text>();
	}

	void Start() {
		text.text = GameMemory.Instance.PlayerHex(gameId);
	}
}