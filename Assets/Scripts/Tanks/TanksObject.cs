using System;
using UnityEngine;

[Serializable]
public enum TanksObjectId
{
	Player, //sprite replaced
	Ground,
	Enemy,
	Dirt,
	Bullet,
}

public class TanksObject : GlitchyObject
{
	public override GameId GameId => GameId.Tanks;

	[SerializeField]
	private SpaceObjectId objectId;

	public override int ObjectId => (int)objectId;
}