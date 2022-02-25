using System.Collections.Generic;
using UnityEngine;

public enum GameId {
	None = -1,
	Platformer,
	SpaceShooter,
	Tanks,
}

public class GameCollection : MonoBehaviour {
	public static GameCollection Instance { get; private set; }

	[SerializeField]
	private List<GameCartridge> cartridges;

	void Awake() {
		Instance = this;
	}

	public GameCartridge Cartridge(GameId gameId) => cartridges[(int)gameId];
}