using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class GlitchySprite : MonoBehaviour {
	/*
	[field: SerializeField]
	[SerializeProperty(nameof(OriginalSprite))]
	private Sprite originalSprite;
	public Sprite OriginalSprite
	{
		get => spriteRenderer.sprite;
		set
		{
			originalSprite = value;
			Sprite = IsTinted ? spriteSheet.Grey : spriteSheet.Original;
		}
	}
	*/
	protected abstract GameId GameId { get; }



	[SerializeField]
	private SpriteRenderer spriteRenderer;

	
	
	public Sprite Sprite { get => spriteRenderer.sprite; private set => spriteRenderer.sprite = value; }
	
	[SerializeField]
	[SerializeProperty(nameof(SpriteSheet))]
	private SpriteSheet spriteSheet;
	public SpriteSheet SpriteSheet { 
		get => spriteSheet; 
		set { 
			spriteSheet = value;
			//Debug.Log($"Setting spritesheet original:{spriteSheet.Original.texture.name}, grey:{spriteSheet.Grey.texture.name}");
			Sprite = IsTinted ? spriteSheet.Grey : spriteSheet.Original;
		} 
	}

	public abstract int ColorId { get; }

	public Color Color => GameCollection.Instance.Cartridge(GameId).ColorPalette[ColorId];

	public bool IsTinted => Sprite == spriteSheet.Grey;

	void Awake() {
		if( spriteRenderer == null )
			spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void Tint(Color color) {
		// Debug.Log($"Tinting {name}");
		if( color == Color ) {
			spriteRenderer.sprite = spriteSheet.Original;
			spriteRenderer.color = Color.white;
		}
		else {
			spriteRenderer.sprite = spriteSheet.Grey;
			spriteRenderer.color = color;
		}
	}

	public void OverrideSprite(GlitchySprite glitchySprite) {
		SpriteSheet = glitchySprite.SpriteSheet;
		if( SpriteSheet.Original.texture == null || SpriteSheet == null)
        {
			SpriteSheet = GameMemory.Instance.MissingSpriteSheet;
		} 
	}
}