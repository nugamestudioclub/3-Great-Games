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

	void Awake() {
		if( glitchySprite == null )
			glitchySprite = GetComponentInChildren<GlitchySprite>();
		IsActive = true;
	}

	void Start() {
		GameMemory.Instance.Subscribe(this);
	}

	public void Refresh() {
		if( spriteOnly ) {
			var newObject = GameMemory.Instance.Object(ToHex);

			//if( !(ToHex == newObject.ToHex && GlitchySprite.Sprite == newObject.GlitchySprite.Sprite)  )
				glitchySprite.OverrideSprite(newObject.GlitchySprite);

			glitchySprite.Tint(GameMemory.Instance.Color(ColorId));
		}
		else {
			/*
			Instantiate(GameMemory.Instance.Object(ToHex), transform.position, transform.rotation);
			IsActive = false;
			Destroy(this);
			*/
		}
	}
}