using UnityEditor;
using UnityEngine;
public abstract class GlitchyObject : MonoBehaviour, IRefreshable, IMemorable {
	[SerializeField]
	private bool spriteOnly;

	private bool isAwake = false;

	public abstract GameId GameId { get; }
	public abstract int ObjectId { get; }

	public int ColorId => glitchySprite.ColorId;

	public int AudioId { get; }

	public string ToHex => $"2{ObjectId}{ColorId}{AudioId}";

	[SerializeField]
	private GlitchySprite glitchySprite;

	void Awake() {
		if( glitchySprite == null )
			glitchySprite = GetComponentInChildren<GlitchySprite>();
		isAwake = true;
	}

	void Start() {
		GameMemory.Instance.Subscribe(this);
	}

	public void Refresh() {
		if( isAwake ) {
			glitchySprite.Tint(GameMemory.Instance.ColorPalette[ColorId]);
			if( spriteOnly ) {

			}
			else {
				// Instantiate(GameMemory.Instance.Object(ObjectId), transform.position, transform.rotation);
				// Destroy(gameObject);
			}
		}
	}
}