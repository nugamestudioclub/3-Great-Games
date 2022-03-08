using UnityEngine;

public class Entity : MonoBehaviour, IRefreshable, IMemorable
{
	[SerializeField]
	private EntityData template;

	private NewGlitchySprite glitchySprite;

	private Color color;

	void Awake() {
		glitchySprite = GetComponentInChildren<NewGlitchySprite>();
		IsActive = true;
	}

	void Start() {
		var cartridge = GameCollection.Instance.Cartridge(template.GameId);

		color = cartridge.ColorPalette[template.ColorId];
		glitchySprite.Color = color;
		glitchySprite.Draw(template.SpriteSheet);

		GameMemory.Instance.Subscribe(this);
	}

	public bool IsActive { get; set; }
	public void Refresh()
	{
		//try
		{
			var newObject = GameMemory.Instance.Object(ToHex);
			EntityData newEntity = GameMemory.Instance.EntityData(ToHex);
			Debug.Log($"Same spritesheet? template :{template.SpriteSheet.Original.texture.name} new :{newEntity.SpriteSheet.Original.texture.name}");

			if (template.SpriteOnly)
			{
				Debug.Log($"Calling refresh on sprite only: Hexcode {ToHex}");
				//if( !(ToHex == newObject.ToHex && GlitchySprite.Sprite == newObject.GlitchySprite.Sprite)  )
				

				glitchySprite.Tint(GameMemory.Instance.Color(template.ColorId));
				glitchySprite.Draw(newEntity.SpriteSheet);
				Debug.Log($"Finishing Calling refresh on sprite only: Hexcode {ToHex}");
			}
			else
			{
				/*
				bool valid = newObject.GameId switch
				{
					//GameId.Platformer => platformerReplacements.Contains((PlatformerObjectId)ObjectId),
					//GameId.SpaceShooter => spaceShooterReplacements.Contains((SpaceObjectId)ObjectId),
					//GameId.Tanks => tanksReplacements.Contains((TanksObjectId)ObjectId),
					_ => false
				};

				if (valid)
				{
					Instantiate(GameMemory.Instance.Object(newObject.ToHex), transform.position, transform.rotation);
					IsActive = false;
					Destroy(this);
				}
				else
				{
					Debug.Log(newObject.ToHex);
				}
				*/
			}
		}
		//catch { }
	}

	public string ToHex =>
		$"{GameMemory.IntToHex((int)template.GameId)}" +
		$"{GameMemory.IntToHex(template.EntityId)}" +
		$"{GameMemory.IntToHex(template.ColorId)}" +
		$"{GameMemory.IntToHex(0)}"; //audio id
}