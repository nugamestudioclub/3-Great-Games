using UnityEngine;

public class Entity : MonoBehaviour {
	[SerializeField]
	private EntityData template;

	private SpriteRenderer spriteRenderer;

	private SpriteSheet spriteSheet;

	private Sprite Sprite {
		get => spriteRenderer.sprite;
		set => spriteRenderer.sprite = value;
	}

	private Color color;

	void Awake() {
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}

	void Start() {
		var cartridge = GameCollection.Instance.Cartridge(template.GameId);

		color = cartridge.ColorPalette[template.ColorId];

	}

	public void Redraw(SpriteSheet spriteSheet) {
		this.spriteSheet = spriteSheet;
	}

	public void Tint(Color color) {
		if( color == this.color ) {
			Sprite = spriteSheet.Original;
			spriteRenderer.color = Color.white;
		}
		else {
			Sprite = spriteSheet.Grey;
			spriteRenderer.color = color;
			this.color = color;
		}
	}
}