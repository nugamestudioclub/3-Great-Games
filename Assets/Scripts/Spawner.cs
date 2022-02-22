using UnityEngine;

public abstract class Spawner : MonoBehaviour {
	protected abstract int TypeId { get; }

	void Start() {
		Instantiate(
			MinigameController.Instance.GameObject(TypeId),
			transform.position,
			transform.rotation
		);
	}
}