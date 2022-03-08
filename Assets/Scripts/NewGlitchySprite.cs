using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class NewGlitchySprite : MonoBehaviour {
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
			Debug.Log($"Trying to set spritesheet of {name}");
			Debug.Log($"Setting spritesheet original:{spriteSheet.Original.texture.name}, grey:{spriteSheet.Grey.texture.name}");
			Sprite = IsTinted ? spriteSheet.Grey : spriteSheet.Original;
			//Sprite = spriteSheet.Grey;
		} 
	}

	public Color Color { get; set; }

	public bool IsTinted => Sprite == spriteSheet.Grey;

	public void Draw(SpriteSheet spriteSheet) {
		SpriteSheet = spriteSheet;
	}

	public void Tint(Color color) {
		Debug.Log($"{transform.parent.name} is tinting");
		if ( color == Color ) {
			Sprite = spriteSheet.Original;
			spriteRenderer.color = Color.white;
		}
		else {
			Sprite = spriteSheet.Grey;
			spriteRenderer.color = color;
			Color = color;
		}
	}
}