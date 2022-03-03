using System;
using UnityEngine;

[CreateAssetMenu(
	fileName = nameof(MultiSpriteSheet),
	menuName = Paths.SCRIPTABLE_SPRITE_SHEETS + "/" + nameof(MultiSpriteSheet))
]
public class MultiSpriteSheet : SpriteSheet {
	[SerializeField]
	private int id;

	[SerializeField]
	private SpriteSheet[] spriteSheets;

	public override Sprite Original => spriteSheets[id].Original;

	public override Sprite Grey => spriteSheets[id].Grey;
}