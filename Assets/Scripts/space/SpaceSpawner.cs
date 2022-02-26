using System;
using UnityEngine;

[Serializable]
public enum SpaceObjectId
{
	Player, //sprite replaced
	Stars,
	Spider,
	Shooter,
	PlayerBullet,
	EnemyBullet,
}

public class SpaceSpawner : Spawner
{
	protected override GameId GameId => GameId.SpaceShooter;

	[SerializeField]
	private SpaceObjectId objectId;

	protected override int ObjectId => (int)objectId;
}