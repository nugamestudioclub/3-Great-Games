using System;
using UnityEngine;

[Serializable]
public enum PlatformerObjectType {
	Coin,
	Brick,
	Goomba
}

public class PlatformerSpawner : Spawner {
	[SerializeField]
	private PlatformerObjectType type;

	protected override int TypeId => (int)type;
}