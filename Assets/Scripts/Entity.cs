using UnityEngine;

public class Entity : MonoBehaviour, IRefreshable, IMemorable {
	[SerializeField]
	private EntityData template;

	//TODO Remove this
	[field: SerializeField]
	public bool CanTransform { get; private set; } = false;

	private GlitchySprite glitchySprite;

	private Color color;

	void Awake() {
		glitchySprite = GetComponentInChildren<GlitchySprite>();
		IsActive = true;
	}

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
			string hex = GameMemory.Instance.MemoryItem(ToHex).ToHex;
			GameId currentGame = template.GameId;
			GameId playerGameId = GameMemory.Instance.GameOfPlayer(hex);
			if (CanTransform && GameMemory.Instance.IsPlayer(hex) && currentGame != playerGameId)
			{

				Debug.Log($"{hex} is the {playerGameId} player!");
				GameObject player = GameCollection.Instance.Cartridge(playerGameId).Player.gameObject;
				Instantiate(player, transform.position, Quaternion.identity);
				Deactivate();
				Destroy(gameObject);
			}
			else if (CanTransform) {
				Debug.Log($"{hex} is not a player!");
			}
			else
			{
				var color = GameMemory.Instance.Color(template.ColorId);
				if (glitchySprite == null)
				{
					Debug.Log($"{name} has no glitchy sprite");
				}
				else
				{
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