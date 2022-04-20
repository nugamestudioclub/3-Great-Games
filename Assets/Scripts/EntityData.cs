using System;
using UnityEngine;

[Serializable]
public abstract class EntityData : ScriptableObject, IMemorable {
	public abstract GameId GameId { get; }

	public abstract int EntityId { get; }

	public abstract int ColorId { get; }

	[field: SerializeField]
	public SpriteSheet SpriteSheet { get; private set; }

    public string ToHex =>
		$"{GameMemory.IntToHex((int)GameId)}" +
		$"{GameMemory.IntToHex(EntityId)}" +
		$"{GameMemory.IntToHex(ColorId)}" +
		$"{GameMemory.IntToHex(0)}";
}