using UnityEngine;

public abstract class Spawner : MonoBehaviour {
	protected abstract GameId GameId { get; }

	protected abstract int ObjectId { get; }

	void Start() {
		Instantiate(
			GameCollection.Instance.Cartridge(GameId).GameObject(ObjectId),
			transform.position,
			transform.rotation
		);
	}
}