using System;
using System.Collections.Generic;
#if UNITY_EDITOR
#endif
using UnityEngine;

[CreateAssetMenu(
	fileName = nameof(TileSpriteSheet),
	menuName = Paths.SCRIPTABLE_SPRITE_SHEETS + "/" + nameof(TileSpriteSheet))
]
public class TileSpriteSheet : GroupSpriteSheet {
	[ReadOnly]
	[SerializeField]
	private SingleSpriteSheet[] spriteSheets = new SingleSpriteSheet[Enum.GetValues(typeof(TileType)).Length];

	protected override IList<SpriteSheet> SpriteSheets => spriteSheets;

	protected override string FolderName() => "Tiles";

	protected override string TypeName(int index) => Enum.GetName(typeof(TileType), index);
}