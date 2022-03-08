using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public abstract class SpriteSheetGroup : ScriptableObject {
	[SerializeField]
	[SerializeProperty(nameof(DefaultSprite))]
	private Sprite defaultSprite;
	public Sprite DefaultSprite {
		get => defaultSprite;
		set {
			defaultSprite = value;
			Debug.Log($"{name} has legnth of: {Count}");
			for( int i = 0; i < Count; i++)
            {
				Debug.Log($"Making {(TileType) i} spritesheet");
				this[i] = MakeSpriteSheet(i, defaultSprite);
			}
				
		}
	}

	protected abstract IList<SingleSpriteSheet> SpriteSheets { get; }

	public int Count => SpriteSheets.Count;

	protected SingleSpriteSheet this[int index] {
		get => SpriteSheets[IndexOrZero(index)];
		set => SpriteSheets[IndexOrZero(index)] = value;
	}

	public Sprite OriginalSprite(int index) => this[index].Original;

	public Sprite GreySprite(int index) => this[index].Grey;

	private SingleSpriteSheet MakeSpriteSheet(int index, Sprite defaultSprite) {
		Sprite sprite = null;
		SingleSpriteSheet spriteSheet = CreateInstance<SingleSpriteSheet>();

#if UNITY_EDITOR
		string defaltSpritePath = AssetDatabase.GetAssetPath(defaultSprite);
		string directory = Path.GetDirectoryName(defaltSpritePath);
		string groupName = GroupName(defaltSpritePath);
		string typeName = TypeName(index);
		string ext = Path.GetExtension(defaltSpritePath);
		string spritePath = SpritePath(directory, groupName, typeName, ext);
		string spriteSheetPath = SpriteSheetPath(FolderName(), groupName, typeName);

		sprite = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);

		Debug.Log($"{spriteSheetPath} is the path!");
		
		CreateAsset(ref spriteSheet, spriteSheetPath);
#endif
		if (sprite != null)
		{
			Debug.Log($"Trying to use: {sprite.texture.name} at {index}");
		} else
        {
			//Debug.Log($"Trying to use: {defaultSprite.texture.name} at {index}");
		}
		spriteSheet.OriginalSprite = sprite == null ? defaultSprite : sprite;

		Debug.Log($"Trying to use: {spriteSheet.OriginalSprite.texture.name} at {index}");

		return spriteSheet;
	}

	private int IndexOrZero(int index) => 0 <= index && index < Count ? index : 0;

	protected abstract string FolderName();

	private static string GroupName(string fileName) {
		return FileSystem.WithoutSuffix(Path.GetFileNameWithoutExtension(fileName), "_");
	}

	protected abstract string TypeName(int index);

	private static string SpritePath(string directory, string groupName, string typeName, string ext) {
		return $"{directory}/{groupName}_{typeName}{ext}";
	}

	private static string SpriteSheetPath(string folderName, string groupName, string typeName) {
		return $"{Paths.SPRITE_SHEETS}/{folderName}/{groupName}/{groupName}_{typeName}.asset";
	}

	private static void CreateAsset(ref SingleSpriteSheet spriteSheet, string path) {
		string absolutePath = $"{Directory.GetCurrentDirectory()}/{path}";
		if (File.Exists(absolutePath))
		{
			spriteSheet = AssetDatabase.LoadAssetAtPath<SingleSpriteSheet>(path);
		} else
        {
			if ( !Directory.Exists(absolutePath) )
				Directory.CreateDirectory(absolutePath);

			AssetDatabase.CreateAsset(spriteSheet, path);
		}
		
	}
}