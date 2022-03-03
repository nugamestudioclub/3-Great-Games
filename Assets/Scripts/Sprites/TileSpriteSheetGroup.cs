using System;
using System.Collections.Generic;
#if UNITY_EDITOR
#endif
using UnityEngine;

[CreateAssetMenu(
	fileName = nameof(TileSpriteSheetGroup),
	menuName = Paths.SCRIPTABLE_SPRITE_SHEETS + "/" + nameof(TileSpriteSheetGroup))
]
public class TileSpriteSheetGroup : SpriteSheetGroup {
	[ReadOnly]
	[SerializeField]
	private SingleSpriteSheet[] spriteSheets = new SingleSpriteSheet[Enum.GetValues(typeof(TileType)).Length];

	protected override IList<SingleSpriteSheet> SpriteSheets => spriteSheets;

	protected override string FolderName() => "Tiles";

	protected override string TypeName(int index) => Enum.GetName(typeof(TileType), index);
}