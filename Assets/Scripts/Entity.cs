using UnityEngine;
using UnityEngine.Tilemaps;

public class Entity : MonoBehaviour, IRefreshable, IMemorable {
	[SerializeField]
	public EntityData template;

	//TODO Remove this
	[field: SerializeField]
	public bool CanTransform { get; private set; } = false;

	[field: SerializeField]
	public bool IsBrick { get; private set; } = false;

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
		glitchySprite.OriginalColor = color;
		glitchySprite.Draw(template.SpriteSheet);
		GameMemory.Instance.Subscribe(this);
	}

	public bool IsActive { get; private set; }

	public void Activate() => IsActive = true;

	public void Deactivate() => IsActive = false;

	public void Refresh() {
		{
			Entity newEntity = GameMemory.Instance.DynamicEntity(ToHex);
			EntityData newEntityData = GameMemory.Instance.DynamicEntityData(ToHex);
			string hex = GameMemory.Instance.MemoryItem(ToHex).ToHex;
			GameId currentGameId = template.GameId;
			GameId playerGameId = GameMemory.Instance.GameOfPlayer(hex);
			if (CanTransform && GameMemory.Instance.IsPlayer(hex) && currentGameId != playerGameId)
			{
				Debug.Log($"{hex} is the {playerGameId} player!");

				var playerTemplate = GameCollection.Instance.Cartridge(playerGameId).Player.gameObject;
				var player = Instantiate(playerTemplate, transform.position, Quaternion.identity);

				Zone.Instance.Player = player.GetComponent<Entity>();

				Deactivate();
				Destroy(gameObject);
			}
			else if (CanTransform && IsBrick && !newEntity.ToHex.Equals(ToHex))
            {
				//disconnect
				gameObject.SetActive(false);

				var entityObject = Instantiate(newEntity.gameObject, transform.position, Quaternion.identity);
				Debug.Log($"{name} was swapped with {entityObject.name}");
				
				var cellPos = Zone.Instance.Tilemap.WorldToCell(transform.position);
				
				Deactivate();
				//Destroy(gameObject);
				Zone.Instance.Tilemap.SetTile(cellPos, null);

			}

			var color = GameMemory.Instance.Color(template.ColorId);
			if( glitchySprite == null ) {
				//Debug.Log($"{name} has no glitchy sprite");
			}
			else {
				if(newEntityData == null )
					Debug.Log($"{name} failed to draw");
				else
					glitchySprite.Draw(newEntityData.SpriteSheet);
				if( color == null )
					Debug.Log($"{name} failed to tint");
				else
					glitchySprite.Tint(color);

			}

		}
	}

	public string ToHex =>
	$"{GameMemory.IntToHex((int)template.GameId)}" +
	$"{GameMemory.IntToHex(template.EntityId)}" +
	$"{GameMemory.IntToHex(template.ColorId)}" +
	$"{GameMemory.IntToHex(0)}"; //audio id


}