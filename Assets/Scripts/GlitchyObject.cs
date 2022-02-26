using UnityEditor;
using UnityEngine;
public abstract class GlitchyObject : MonoBehaviour, IRefreshable, IMemorable {
	public abstract GameId GameId { get; }
	public abstract int ObjectId { get; }
	public int ColorId => glitchySprite.ColorId;
	public int AudioId { get; }

	public string HexCode => $"2{ObjectId}{ColorId}{AudioId}";


	private GlitchySprite glitchySprite;

	void Awake() {
		glitchySprite = GetComponent<GlitchySprite>();
	}

	void Start() {
		GameMemory.Instance.Store(this);
	}

	public void Refresh() {
		glitchySprite.Tint(GameMemory.Instance.Color(GameId, ColorId));
	}

}