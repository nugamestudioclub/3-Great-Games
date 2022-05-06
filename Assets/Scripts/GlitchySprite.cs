using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GlitchySprite : MonoBehaviour {
	private SpriteRenderer spriteRenderer;
	private int index;

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
			UpdateSprite();
		} 
	}

    private void UpdateSprite()
    {
		Sprite = IsTinted ? spriteSheet.GreyAt(index) : spriteSheet.OriginalAt(index);
	}
	public  Color OriginalColor { get; set; }
	public Color Color { get; set; }

	public bool IsTinted => Color != OriginalColor;

	public void Draw(SpriteSheet spriteSheet) {
		SpriteSheet = spriteSheet;
	}

	public void Draw(SpriteSheet spriteSheet, int index)
	{
		this.index = index;
		SpriteSheet = spriteSheet;
	}

	public void DrawFrame(int index)
	{
		this.index = index;
		Sprite = IsTinted ? spriteSheet.FindUniqueGrey(index) : spriteSheet.FindUniqueOriginal(index);
	}

	public void Tint(Color color) {
		Color = color;
		if ( Color == OriginalColor ) {
			Sprite = spriteSheet.OriginalAt(index);
			spriteRenderer.color = Color.white;
		}
		else {
			Sprite = spriteSheet.GreyAt(index);
			spriteRenderer.color = Color;
		}
	}
}