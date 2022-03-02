using UnityEngine;
public static class Paths {
	public const string SCRIPTABLE_OBJECTS = nameof(ScriptableObject) + "s";

	public const string ASSETS = "Assets";

	public const string SPRITES = ASSETS + "/Sprites";

	public const string SPRITE_SHEETS = SPRITES + "/" + nameof(SpriteSheet) + "s";

	public const string SCRIPTALBE_SPRITE_SHEETS = SCRIPTABLE_OBJECTS + "/" + nameof(SpriteSheet) + "s";
}