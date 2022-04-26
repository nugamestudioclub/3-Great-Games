using System;
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

	public GameId GameId(int id) => (GameId)(id % Count);

	public GameCartridge Cartridge(int gameId) => cartridges[gameId % Count];
	public GameCartridge Cartridge(GameId gameId) => Cartridge((int)gameId);

	public static int Count => Enum.GetValues(typeof(GameId)).Length - 1;
}