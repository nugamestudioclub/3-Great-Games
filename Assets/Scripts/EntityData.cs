using System;
using UnityEngine;

[Serializable]
public abstract class EntityData : ScriptableObject {
	[field: SerializeField]
	public GameId GameId { get; set; }

	public abstract int EntityId { get; }

	public abstract int ColorId { get; }

	[field: SerializeField]
	public SpriteSheet SpriteSheet { get; private set; }
}