using UnityEditor;
using UnityEngine;
public abstract class GlitchyObject : MonoBehaviour, IRefreshable, IMemorable {
	[SerializeField]
	private bool spriteOnly;
	public abstract GameId GameId { get; }

	public abstract int ObjectId { get; }

	public int ColorId => glitchySprite.ColorId;

	public int AudioId { get; }

	public string ToHex => $"{ObjectId.ToString().PadLeft(2, '0')}{ColorId}{AudioId}";

	[SerializeField]
	private GlitchySprite glitchySprite;
	public GlitchySprite GlitchySprite => glitchySprite;

	void Awake() {
		if( glitchySprite == null )
			glitchySprite = GetComponentInChildren<GlitchySprite>();
	}

	void Start() {
		GameMemory.Instance.Subscribe(this);
	}

	public void Refresh() {
		if( spriteOnly ) {
			var newObject = GameMemory.Instance.Object(ToHex);

			//if( !(GameId == newObject.GameId && ObjectId == newObject.ObjectId) )
			glitchySprite.OverrideSprite(newObject.GlitchySprite);

			glitchySprite.Tint(GameMemory.Instance.Color(ColorId));
		}
		else {
			// Instantiate(GameMemory.Instance.Object(ObjectId), transform.position, transform.rotation);
			// Destroy(gameObject);
		}
	}

	public static int HexToId(string hex) => GameMemory.HexToInt(hex.Substring(0, 2));
}