using System;
using UnityEngine;

[Serializable]
public enum PlatformerObjectId {
	Coin,
	Brick,
	Goomba
}

public class PlatformerSpawner : Spawner {
	protected override GameId GameId => GameId.Platformer;

	[SerializeField]
	private PlatformerObjectId objectId;

	protected override int ObjectId => (int)objectId;
}