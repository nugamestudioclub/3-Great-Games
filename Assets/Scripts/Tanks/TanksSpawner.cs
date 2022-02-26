using System;
using UnityEngine;

[Serializable]
public enum TanksObjectId
{
	Player, //sprite replaced
	PlayerGun,
	Ground,
	Enemy,
	Dirt,
	Bullet,
}

public class TanksSpawner : Spawner
{
	protected override GameId GameId => GameId.Tanks;

	[SerializeField]
	private TanksObjectId objectId;

	protected override int ObjectId => (int)objectId;
}