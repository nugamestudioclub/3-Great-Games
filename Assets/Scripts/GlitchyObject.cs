using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public abstract class GlitchyObject : MonoBehaviour, IRefreshable, IMemorable {
	[SerializeField]
	private bool spriteOnly;

	public abstract GameId GameId { get; }

	public abstract int ObjectId { get; }

	public int ColorId => glitchySprite.ColorId;

	public int AudioId { get; }

	public bool IsActive { get; set; }

	public string ToHex =>
		$"{GameMemory.IntToHex((int)GameId)}" +
		$"{GameMemory.IntToHex(ObjectId)}" +
		$"{GameMemory.IntToHex(ColorId)}" +
		$"{GameMemory.IntToHex(AudioId)}";

	[SerializeField]
	private GlitchySprite glitchySprite;
	public GlitchySprite GlitchySprite => glitchySprite;

	[SerializeField]
	private List<PlatformerObjectId> platformerReplacements;

	[SerializeField]
	private List<SpaceObjectId> spaceShooterReplacements;

	[SerializeField]
	private List<TanksObjectId> tanksReplacements;

	void Awake() {
		if( glitchySprite == null )
			glitchySprite = GetComponentInChildren<GlitchySprite>();
		IsActive = true;
	}

	void Start() {
		//GameMemory.Instance.Subscribe(this);
	}

	public void Refresh() {
		try {
			var newObject = GameMemory.Instance.Object(ToHex);

			if( spriteOnly ) {
				// Debug.Log($"Calling refresh on sprite only: Hexcode {ToHex}");
				//if( !(ToHex == newObject.ToHex && GlitchySprite.Sprite == newObject.GlitchySprite.Sprite)  )
				//glitchySprite.OverrideSprite(newObject.GlitchySprite);

				glitchySprite.Tint(GameMemory.Instance.Color(ColorId));
				// Debug.Log($"Finishing Calling refresh on sprite only: Hexcode {ToHex}");
			}
			else {
				bool valid = newObject.GameId switch {
					GameId.Platformer => platformerReplacements.Contains((PlatformerObjectId)ObjectId),
					GameId.SpaceShooter => spaceShooterReplacements.Contains((SpaceObjectId)ObjectId),
					GameId.Tanks => tanksReplacements.Contains((TanksObjectId)ObjectId),
					_ => false
				};

				if( valid ) {
					Instantiate(GameMemory.Instance.Object(newObject.ToHex), transform.position, transform.rotation);
					IsActive = false;
					Destroy(this);
				}
				else {
					// Debug.Log(newObject.ToHex);
				}
			}
		}
		catch { }
	}
}