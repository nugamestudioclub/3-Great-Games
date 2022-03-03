using System;
using UnityEngine;

public enum TileType {
	// standard
	Block,  // 4 edges
	Center, // no edges

	// edges
	EdgeLeft,
	EdgeRight,
	EdgeTop,
	EdgeTopLeft,
	EdgeTopRight,
	EdgeBottom,
	EdgeBottomLeft,
	EdgeBottomRight,

	// inner corners
	InnerTopLeft,
	InnerTopRight,
	InnerBottomLeft,
	InnerBottomRight,

	// pipes (one thick, two non adjacent exists)
	PipeHorizontal,
	PipeVertical,

	//joint (one thick, two adjacent exits)
	JointTopLeft,
	JointTopRight,
	JointBottomLeft,
	JointBottomRight,

	// T (one thick, three exits)
	TLeft,
	TRight,
	TTop,
	TBottom,

	// cross (one thick, 4 exits)
	Cross,

	// caps (one thick, one open side)
	CapLeft,
	CapRight,
	CapTop,
	CapBottom,
}

[Serializable]
[CreateAssetMenu(
	fileName = nameof(TileSpriteSheet),
	menuName = Paths.SCRIPTABLE_SPRITE_SHEETS + "/" + nameof(TileSpriteSheet))
]
public class TileSpriteSheet : SpriteSheet {
	[SerializeField]
	private TileType type;

	[SerializeField]
	private TileSpriteSheetGroup sprites;

	public override Sprite Original => sprites.OriginalSprite((int)type);

	public override Sprite Grey => sprites.GreySprite((int)type);
}