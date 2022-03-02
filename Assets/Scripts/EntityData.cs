using System;
using UnityEngine;

[Serializable]
public abstract class EntityData : ScriptableObject {
	public abstract GameId GameId { get; }

	public abstract int EntityId { get; }

	public abstract int ColorId { get; }

	[field: SerializeField]
	public SpriteSheet SpriteSheet { get; private set; }

	//TODO Remove this
	[field: SerializeField]
	public bool SpriteOnly { get; private set; } = true;
}