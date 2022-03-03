using System;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[Serializable]
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