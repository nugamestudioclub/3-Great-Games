using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class GlitchySprite : MonoBehaviour {
	protected abstract GameId GameId { get; }

	[SerializeField]
	private SpriteRenderer spriteRenderer;
	private Sprite mainSprite;
	private Sprite greySprite;

	public Sprite Sprite { get => spriteRenderer.sprite; private set => spriteRenderer.sprite = value; }

	public abstract int ColorId { get; }

	public Color Color => GameCollection.Instance.Cartridge(GameId).ColorPalette[ColorId];

	public bool IsTinted => Sprite == greySprite;

	void Awake() {
		if( spriteRenderer == null )
			spriteRenderer = GetComponent<SpriteRenderer>();
		mainSprite = spriteRenderer.sprite;
		greySprite = GreySprite(mainSprite);
	}

	public void Tint(Color color) {
		if( color == Color ) {
			spriteRenderer.sprite = mainSprite;
			spriteRenderer.color = Color.white;
		}
		else {
			spriteRenderer.sprite = greySprite;
			spriteRenderer.color = color;
		}
	}

	public void OverrideSprite(GlitchySprite glitchySprite) {
		bool isTinted = IsTinted;
		var sprite = glitchySprite.Sprite;

		if( sprite == null )
			sprite = GameMemory.Instance.MissingSprite;

		mainSprite = glitchySprite.Sprite;
		greySprite = GreySprite(mainSprite);

		Sprite = isTinted ? greySprite : mainSprite;
	}

	private static Sprite GreySprite(Sprite mainSprite) {
		return AssetDatabase.LoadAssetAtPath<Sprite>(GreyPath(AssetDatabase.GetAssetPath(mainSprite)));
	}

	private static string GreyPath(string path) {
		int pos = path.LastIndexOf('.');

		return path.Substring(0, pos) + "_grey" + path.Substring(pos, path.Length - pos);
	}
}