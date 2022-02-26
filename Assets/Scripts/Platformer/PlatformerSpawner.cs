using System;
using UnityEngine;

[Serializable]
public enum PlatformerObjectId {
	Player, //sprite replaced
	Grass,
	Dirt,
	Brick,
	Cloud, //game object replaced
	Gem,
	Key,
	Door,
	Pacer,
}

public class PlatformerSpawner : Spawner {
	protected override GameId GameId => GameId.Platformer;

	[SerializeField]
	private PlatformerObjectId objectId;

	protected override int ObjectId => (int)objectId;
}