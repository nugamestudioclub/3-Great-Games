using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GlitchySprite : MonoBehaviour {
	[SerializeField]
	private Color color;

	private SpriteRenderer spriteRenderer;
	private Sprite mainSprite;
	private Sprite greySprite;

	private static string GreyPath(string path) {
		int pos = path.LastIndexOf('.');

		return path.Substring(0, pos) + "_grey" + path.Substring(pos, path.Length - pos);
	}

	void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		mainSprite = spriteRenderer.sprite;
		greySprite = AssetDatabase.LoadAssetAtPath<Sprite>(GreyPath(AssetDatabase.GetAssetPath(mainSprite)));
		spriteRenderer.sprite = greySprite;
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
}