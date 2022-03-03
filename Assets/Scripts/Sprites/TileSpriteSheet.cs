using System;
using UnityEngine;

public enum TileType {
	// standard
	Default, // serialized
	Block,   // 4 edges
	Center,  // no edges

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

	// caps (one thick, one exit)
	CapLeft,
	CapRight,
	CapTop,
	CapBottom,

	// pipes (one thick, two non adjacent exits)
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
	private TileSpriteSheetGroup spriteGroup;

	public override Sprite Original => spriteGroup.OriginalSprite((int)type);

	public override Sprite Grey => spriteGroup.GreySprite((int)type);
}