using System;
using UnityEngine;

[Serializable]
public enum SpaceObjectId
{
	Player, //sprite replaced
	Spider,
	Shooter,
	PlayerBullet,
	EnemyBullet,
}

public class SpaceObject : GlitchyObject
{
	public override GameId GameId => GameId.SpaceShooter;

	[SerializeField]
	private SpaceObjectId objectId;

	public override int ObjectId => (int)objectId;
}