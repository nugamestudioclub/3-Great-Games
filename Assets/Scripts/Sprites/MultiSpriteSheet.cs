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
	private Palette<SpriteSheet> spriteSheets;

	public override Sprite Original => OriginalAt(id);

	public override Sprite Grey => GreyAt(id);

    public override Sprite GreyAt(int index) => spriteSheets[index].Grey;

    public override Sprite OriginalAt(int index) => spriteSheets[index].Original;
}