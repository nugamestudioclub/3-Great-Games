using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[Serializable]
[CreateAssetMenu(
	fileName = nameof(SpriteSheet),
	menuName = Paths.SCRIPTABLE_OBJECTS + "/" + nameof(SpriteSheet))
]
public class SpriteSheet : ScriptableObject, ISpriteSheet {
	[SerializeField]
	[SerializeProperty(nameof(OriginalSprite))]
	private Sprite originalSprite;
	public Sprite OriginalSprite {
		get => originalSprite;
		set {
			originalSprite = value;
			GreySprite = FindGrey(originalSprite);
		}
	}

	[field: ReadOnly]
	[field: SerializeField]
	public Sprite GreySprite { get; private set; }

	private static Sprite FindGrey(Sprite sprite) {
		Sprite greySprite = null;

#if UNITY_EDITOR
		greySprite = AssetDatabase.LoadAssetAtPath<Sprite>(FindGrey(AssetDatabase.GetAssetPath(sprite)));
#endif

		return greySprite == null ? sprite : greySprite;
	}

	private static string FindGrey(string path) {
		return FileSystem.WithSuffix(path, "_grey");
	}
}