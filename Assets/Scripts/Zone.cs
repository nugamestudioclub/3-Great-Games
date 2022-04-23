using UnityEngine;

public class Zone : MonoBehaviour {
	public static Zone Instance { get; private set; }

	[field: SerializeField]
	public Camera PrimaryCamera { get; private set; }

	[field: SerializeField]
	public Entity Player { get; set; }

	void Awake() {
		Instance = this;
	}
}