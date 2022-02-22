using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GlitchySprite : MonoBehaviour {
	[SerializeField]
	private Color color;

	private SpriteRenderer spriteRenderer;
	private Sprite mainSprite;
	private Sprite greySprite;

	public Sprite Sprite { get => spriteRenderer.sprite; private set => spriteRenderer.sprite = value; }

	public bool IsTinted => Sprite == greySprite;

	void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		mainSprite = spriteRenderer.sprite;
		greySprite = GreySprite(mainSprite);
	}

	public void Override(Sprite sprite) {
		bool isTinted = IsTinted;

		mainSprite = sprite;
		greySprite = GreySprite(mainSprite);

		Sprite = isTinted ? greySprite : mainSprite;
	}

	public void Tint(Color color) {
		if( color == this.color ) {
			spriteRenderer.sprite = mainSprite;
			spriteRenderer.color = Color.white;
		}
		else {
			spriteRenderer.sprite = greySprite;
			spriteRenderer.color = color;
		}
	}

	private static Sprite GreySprite(Sprite mainSprite) {
		return AssetDatabase.LoadAssetAtPath<Sprite>(GreyPath(AssetDatabase.GetAssetPath(mainSprite)));
	}

	private static string GreyPath(string path) {
		int pos = path.LastIndexOf('.');

		return path.Substring(0, pos) + "_grey" + path.Substring(pos, path.Length - pos);
	}
}