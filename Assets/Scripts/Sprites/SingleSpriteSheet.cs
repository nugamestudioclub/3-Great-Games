#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CreateAssetMenu(
	fileName = nameof(SingleSpriteSheet),
	menuName = Paths.SCRIPTABLE_SPRITE_SHEETS + "/" + nameof(SingleSpriteSheet))
]
public class SingleSpriteSheet : SpriteSheet {
	static int i = 0;
	[SerializeField]
	[SerializeProperty(nameof(OriginalSprite))]
	private Sprite originalSprite;
	public Sprite OriginalSprite {
		get => originalSprite;
		set {
			originalSprite = value;
			
            // Debug.Log($"{name}{value.texture.name}{ i++}");
            GreySprite = FindGrey(originalSprite);
#if UNITY_EDITOR
			EditorUtility.SetDirty(this);
#endif
		}
	}

	public override Sprite Original => OriginalSprite;

	[field: ReadOnly]
	[field: SerializeField]
	public Sprite GreySprite { get; private set; }

	public override Sprite Grey => GreySprite;

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

	public override Sprite OriginalAt(int index) => Original;
	public override Sprite GreyAt(int index) => Grey;
}