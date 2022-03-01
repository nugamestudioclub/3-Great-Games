using System;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


[Serializable]
[CreateAssetMenu(
	fileName = nameof(TilemapSprites),
	menuName = Paths.SCRIPTABLE_OBJECTS + "/" + nameof(TilemapSprites))
]
public class TilemapSprites : ScriptableObject {

	[SerializeField]
	[SerializeProperty(nameof(DefaultSprite))]
	private Sprite defaultSprite;
	public Sprite DefaultSprite {
		get => defaultSprite;
		set {
			defaultSprite = value;
			for( int i = 0; i < tiles.Length; i++ )
				tiles[i] = FindTile((TileType)i, defaultSprite);
		}
	}

	public Sprite OriginalSprite(TileType type) => tiles[(int)type].OriginalSprite;

	public Sprite GreySprite(TileType type) => tiles[(int)type].GreySprite;

	[ReadOnly]
	[SerializeField]
	private SingleSpriteSheet[] tiles = new SingleSpriteSheet[Enum.GetValues(typeof(TileType)).Length];

	private static SingleSpriteSheet FindTile(TileType type, Sprite sprite) {
		Sprite tileSprite = null;
		SingleSpriteSheet tileSpriteSheet = CreateInstance<SingleSpriteSheet>();

#if UNITY_EDITOR
		string spritePath = AssetDatabase.GetAssetPath(sprite);
		string directory = Path.GetDirectoryName(spritePath);
		string tileName = TileName(spritePath);
		string tileType = Enum.GetName(typeof(TileType), type);
		string ext = Path.GetExtension(spritePath);
		string tilePath = TilePath(directory, tileName, tileType, ext);
		string spriteSheetPath = SpriteSheetPath(tileName, tileType);

		tileSprite = AssetDatabase.LoadAssetAtPath<Sprite>(tilePath);
		CreateAsset(tileSpriteSheet, spriteSheetPath);
#endif

		tileSpriteSheet.OriginalSprite = tileSprite == null ? sprite : tileSprite;

		return tileSpriteSheet;
	}

	private static string TileName(string fileName) {
		return FileSystem.WithoutSuffix(Path.GetFileNameWithoutExtension(fileName), "_");
	}

	private static string TilePath(string folderPath, string tilename, string tiletype, string ext) {
		return $"{folderPath}/{tilename}_{tiletype}{ext}";
	}

	private static string SpriteSheetPath(string tilename, string tiletype) {
		return $"{Paths.SPRITE_SHEETS}/Tiles/{tilename}/{tilename}_{tiletype}.asset";
	}

	private static void CreateAsset(SingleSpriteSheet spriteSheet, string path) {
		string absolutePath = $"{Directory.GetCurrentDirectory()}/{path}";

		if( !Directory.Exists(absolutePath) )
			Directory.CreateDirectory(absolutePath);

		AssetDatabase.CreateAsset(spriteSheet, path);
	}
}