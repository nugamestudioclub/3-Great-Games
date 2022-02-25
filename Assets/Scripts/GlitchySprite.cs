using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class GlitchySprite : MonoBehaviour, IMemorable {
	public Color Color => GameCollection.Instance.Cartridge(GameId).Color(ColorId);

	private SpriteRenderer spriteRenderer;
	private Sprite mainSprite;
	private Sprite greySprite;

	public GameId Id => GameId;

	protected abstract GameId GameId { get; }

	public Sprite Sprite { get => spriteRenderer.sprite; private set => spriteRenderer.sprite = value; }

	public bool IsTinted => Sprite == greySprite;

	void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		mainSprite = spriteRenderer.sprite;
		greySprite = GreySprite(mainSprite);
	}

	void Start() {
		GameMemory.Instance.Store(this);
	}

	public void Refresh() {
		Tint(GameMemory.Instance.Color(GameId, ColorId));
	}

	public void Tint(Color color) {
		if( color == this.Color ) {
			spriteRenderer.sprite = mainSprite;
			spriteRenderer.color = Color.white;
		}
		else {
			spriteRenderer.sprite = greySprite;
			spriteRenderer.color = color;
		}
	}

	public void OverrideSprite(Sprite sprite) {
		bool isTinted = IsTinted;

		mainSprite = sprite;
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

	public abstract int ColorId { get; }
}