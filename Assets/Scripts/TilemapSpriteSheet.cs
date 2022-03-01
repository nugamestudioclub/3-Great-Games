using System;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


[Serializable]
[CreateAssetMenu(
	fileName = nameof(TilemapSpriteSheet),
	menuName = Paths.SCRIPTABLE_OBJECTS + "/" + nameof(TilemapSpriteSheet))
]
public class TilemapSpriteSheet : ScriptableObject, ISpriteSheet {

	[SerializeField]
	[SerializeProperty(nameof(OriginalSprite))]
	private Sprite originalSprite;
	public Sprite OriginalSprite {
		get => originalSprite;
		set {
			originalSprite = value;
			for( int i = 0; i < tiles.Length; i++ )
				tiles[i] = FindTile((TileType)i, originalSprite);
		}
	}

	public Sprite GreySprite => tiles[0].GreySprite;

	public Sprite GetOriginalSprite(TileType type) => tiles[(int)type].OriginalSprite;

	public Sprite GetGreySprite(TileType type) => tiles[(int)type].GreySprite;

	[ReadOnly]
	[SerializeField]
	private SpriteSheet[] tiles = new SpriteSheet[Enum.GetValues(typeof(TileType)).Length];

	private static SpriteSheet FindTile(TileType type, Sprite sprite) {
		Sprite tileSprite = null;
		SpriteSheet tileSpriteSheet = CreateInstance<SpriteSheet>();

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

	private static void CreateAsset(SpriteSheet spriteSheet, string path) {
		string absolutePath = $"{Directory.GetCurrentDirectory()}/{path}";

		if( !Directory.Exists(absolutePath) )
			Directory.CreateDirectory(absolutePath);

		AssetDatabase.CreateAsset(spriteSheet, path);
	}
}