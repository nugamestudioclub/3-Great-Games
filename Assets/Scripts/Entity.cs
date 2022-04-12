using UnityEngine;

public class Entity : MonoBehaviour, IRefreshable, IMemorable {
	[SerializeField]
	private EntityData template;

	//public bool Yes => template.EntityId == (int)PlatformerEntityId.Brick;

	private NewGlitchySprite glitchySprite;

	private Color color;

	void Awake() {
		glitchySprite = GetComponentInChildren<NewGlitchySprite>();
		IsActive = true;
	}

	private string hex;

	void Start() {
		var cartridge = GameCollection.Instance.Cartridge(template.GameId);

		color = cartridge.ColorPalette[template.ColorId];
		glitchySprite.Color = color;
		glitchySprite.Draw(template.SpriteSheet);
		GameMemory.Instance.Subscribe(this);
	}

	public bool IsActive { get; private set; }

	public void Activate() => IsActive = true;

	public void Deactivate() => IsActive = false;

	public void Refresh() {
		{
			EntityData newEntity = GameMemory.Instance.DynamicEntityData(ToHex);

			if (template.SpriteOnly) {

				var color = GameMemory.Instance.Color(template.ColorId);
				if (glitchySprite == null) {
					Debug.Log($"{name} has no glitchy sprite");
				}
				else {
					if (newEntity == null)
						Debug.Log($"{name} failed to draw");
					else
						glitchySprite.Draw(newEntity.SpriteSheet);
                    if (color == null)
                        Debug.Log($"{name} failed to tint");
					else
                        glitchySprite.Tint(color);
					
				}
			}
		}
	}

	public string ToHex =>
	$"{GameMemory.IntToHex((int)template.GameId)}" +
	$"{GameMemory.IntToHex(template.EntityId)}" +
	$"{GameMemory.IntToHex(template.ColorId)}" +
	$"{GameMemory.IntToHex(0)}"; //audio id
}