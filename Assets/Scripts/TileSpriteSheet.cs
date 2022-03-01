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

namespace Assets.Scripts.Utils {
	class TileSpriteSheet : SpriteSheet {

	}
}