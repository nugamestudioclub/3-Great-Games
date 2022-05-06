using UnityEngine;
using UnityEngine.Tilemaps;

public class Entity : MonoBehaviour, IRefreshable, IMemorable {
	[field: SerializeField]
	public EntityData Template { get; private set; }

	//TODO Remove this
	[field: SerializeField]
	public bool CanTransform { get; private set; } = false;

	[field: SerializeField]
	public bool IsBrick { get; private set; } = false;

	protected GlitchySprite GlitchySprite { get; private set; }

	private Color color;

	void Awake() {
		GlitchySprite = GetComponentInChildren<GlitchySprite>();
		IsActive = true;
	}

	void Start() {
		var cartridge = GameCollection.Instance.Cartridge(Template.GameId);

		color = cartridge.ColorPalette[Template.ColorId];

		InitialTint();
		InitialDraw();

		GameMemory.Instance.Subscribe(this);
	}

	protected virtual void InitialDraw() {
		GlitchySprite.Draw(Template.SpriteSheet);
	}

	protected virtual void InitialTint() {
		GlitchySprite.Color = color;
		GlitchySprite.OriginalColor = color;
	}

	protected virtual void Draw(SpriteSheet spriteSheet) {
		GlitchySprite.Draw(spriteSheet);
	}

	protected virtual void Tint(Color color) {
		GlitchySprite.Tint(color);
	}

	public bool IsActive { get; private set; }

	public void Activate() => IsActive = true;

	public void Deactivate() => IsActive = false;

	public void Refresh() {
		{
			Entity newEntity = GameMemory.Instance.DynamicEntity(ToHex);
			EntityData newEntityData = GameMemory.Instance.DynamicEntityData(ToHex);
			string hex = GameMemory.Instance.MemoryItem(ToHex).ToHex;
			GameId currentGameId = Template.GameId;
			GameId playerGameId = GameMemory.Instance.GameOfPlayer(hex);
			if( CanTransform && GameMemory.Instance.IsPlayer(hex) && currentGameId != playerGameId ) {
				Debug.Log($"{hex} is the {playerGameId} player!");

				var playerTemplate = GameCollection.Instance.Cartridge(playerGameId).Player.gameObject;
				var player = Instantiate(playerTemplate, transform.position, Quaternion.identity);

				Zone.Instance.Player = player.GetComponent<Entity>();

				Deactivate();
				Destroy(gameObject);
			}
			else if( CanTransform && IsBrick && !newEntity.ToHex.Equals(ToHex) ) {
				//disconnect
				gameObject.SetActive(false);

				var entityObject = Instantiate(newEntity.gameObject, transform.position, Quaternion.identity);
				Debug.Log($"{name} was swapped with {entityObject.name}");

				var cellPos = Zone.Instance.Tilemap.WorldToCell(transform.position);

				Deactivate();
				//Destroy(gameObject);
				Zone.Instance.Tilemap.SetTile(cellPos, null);

			}

			var color = GameMemory.Instance.Color(Template.ColorId);
			if( GlitchySprite == null ) {
				//Debug.Log($"{name} has no glitchy sprite");
			}
			else {
				if( newEntityData == null )
					Debug.Log($"{name} failed to draw");
				else
					Draw(newEntityData.SpriteSheet);
				if( color == null )
					Debug.Log($"{name} failed to tint");
				else
					Tint(color);

			}

		}
	}

	public string ToHex =>
		$"{GameMemory.IntToHex((int)Template.GameId)}" +
		$"{GameMemory.IntToHex(Template.EntityId)}" +
		$"{GameMemory.IntToHex(Template.ColorId)}" +
		$"{GameMemory.IntToHex(0)}"; //audio id
}