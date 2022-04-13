using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GlitchySprite : MonoBehaviour {
	private SpriteRenderer spriteRenderer;

	void Awake()
	{
		if (spriteRenderer == null)
			spriteRenderer = GetComponent<SpriteRenderer>();
	}

    public Sprite Sprite { get => spriteRenderer.sprite; private set => spriteRenderer.sprite = value; }
	
	private SpriteSheet spriteSheet;
	public SpriteSheet SpriteSheet { 
		get => spriteSheet; 
		set { 
			spriteSheet = value;
			Sprite = IsTinted ? spriteSheet.Grey : spriteSheet.Original;
		} 
	}

	public Color Color { get; set; }

	public bool IsTinted => Sprite == spriteSheet.Grey;

	public void Draw(SpriteSheet spriteSheet) {
		SpriteSheet = spriteSheet;
	}

	public void Tint(Color color) {
		if ( color == Color ) {
			Sprite = spriteSheet.Original;
			spriteRenderer.color = Color.white;
		}
		else {
			Sprite = spriteSheet.Grey;
			spriteRenderer.color = color;
		}
	}
}