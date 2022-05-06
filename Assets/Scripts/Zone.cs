using UnityEngine;
using UnityEngine.Tilemaps;

public class Zone : MonoBehaviour {
	public static Zone Instance { get; private set; }

	[field: SerializeField]
	public Camera PrimaryCamera { get; private set; }

	[SerializeField]
	private Entity player;

	public Entity Player {
		get => player;
		set {
			player = value;
			if( PrimaryCamera.gameObject.TryGetComponent(out Follower follower) )
				follower.Follow(value.transform);
		}
	}

	[field:SerializeField]
	public Tilemap Tilemap { get; private set; }

	void Awake() {
		Instance = this;
	}
}