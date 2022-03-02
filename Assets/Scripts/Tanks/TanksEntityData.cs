using System;
using UnityEngine;

public enum TanksEntityId {
	Player,
	Brick,
	Enemy,
	Dirt,
	Bullet,
}

public enum TanksColorId {
	Brown,
	DarkGreen,
	LightBlue,
	Yellow,
	Green,
	DarkBlue,
	Orange,
	H,
	I,
	J,
	K,
	L,
	M,
	N,
	O,
}

[Serializable]
[CreateAssetMenu(
	fileName = nameof(TanksEntityData),
	menuName = Paths.SCRIPTABLE_OBJECTS + "/" + nameof(EntityData) + "/" + nameof(TanksEntityData))
]
public class TanksEntityData : EntityData {
	[SerializeField]
	private TanksEntityId entityId;
	public override int EntityId => (int)entityId;

	[SerializeField]
	private TanksColorId colorId;
	public override int ColorId => (int)colorId;
}