using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(
	fileName = nameof(MultiSpriteSheet),
	menuName = Paths.SCRIPTABLE_OBJECTS + "/" + nameof(MultiSpriteSheet))
]
public class MultiSpriteSheet : SpriteSheet {
	[SerializeField]
	private int id;

	[SerializeField]
	private SpriteSheet[] spriteSheets;

	public override Sprite Original => spriteSheets[id].Original;

	public override Sprite Grey => spriteSheets[id].Grey;
}