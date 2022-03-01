using System;
using UnityEngine;

public enum TileType {
	//standard
	Block, //4 edges
	Center, //no edges

	//edges
	EdgeLeft,
	EdgeRight,
	EdgeTop,
	EdgeTopLeft,
	EdgeTopRight,
	EdgeBottom,
	EdgeBottomLeft,
	EdgeBottomRight,

	//inner corners
	InnerTopLeft,
	InnerTopRight,
	InnerBottomLeft,
	InnerBottomRight,

	//joint (one thick, two adjacnet exits)
	JointTopLeft,
	JointTopRight,
	JointBottomLeft,
	JointBottomRight,

	//pipes (one thick, two non adjacent exists)
	PipeHorizontal,
	PipeVertical,

	//T (one thick, three exits)
	TLeft,
	TRight,
	TTop,
	TBottom,

	//cross (one thick, 4 exits)
	Cross,
}

[Serializable]
[CreateAssetMenu(
	fileName = nameof(TileSpriteSheet),
	menuName = Paths.SCRIPTABLE_OBJECTS + "/" + nameof(TileSpriteSheet))
]
public class TileSpriteSheet : SpriteSheet {
	[SerializeField]
	private TileType type;

	[SerializeField]
	private TilemapSprites sprites;

	public override Sprite Original => sprites.OriginalSprite(type);

	public override Sprite Grey => sprites.GreySprite(type);
}