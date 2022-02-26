using UnityEngine;

public class GameStart : MonoBehaviour {
	[SerializeField]
	private GameId gameId;

	void Start() {
		GameMemory.Instance.Load(gameId);
	}
}