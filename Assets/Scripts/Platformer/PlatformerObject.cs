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
	Ladybug,
}

public class PlatformerObject : GlitchyObject
{
	public override GameId GameId => GameId.Platformer;

	[SerializeField]
	private PlatformerObjectId objectId;

	public override int ObjectId => (int)objectId;
}