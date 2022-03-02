using System;
using UnityEngine;

public enum SpaceEntityId {
	Player,
	Spider,
	Shooter,
	PlayerBullet,
	EnemyBullet,
}

public enum SpaceColorId {
	LightLavender,
	Lavender,
	Cyan,
	Red,
	LightPink,
	Pink,
	White,
	Green,
	Orange,
	Mauve,
	LightBlue,
	Blue,
	Yellow,
	Yellow2,
	Blue2,
	DarkBlue,
}

[Serializable]
[CreateAssetMenu(
	fileName = nameof(SpaceEntityData),
	menuName = Paths.SCRIPTABLE_OBJECTS + "/" + nameof(EntityData) + "/" + nameof(SpaceEntityData))
]
public class SpaceEntityData : EntityData {
	[SerializeField]
	private SpaceEntityId entityId;
	public override int EntityId => (int)entityId;

	[SerializeField]
	private SpaceColorId colorId;
	public override int ColorId => (int)colorId;
}