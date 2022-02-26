using UnityEngine;

public  class Spawner : MonoBehaviour {

	[SerializeField]
	private GlitchyObject obj;

	void Start() {/*
		Instantiate(
			GameCollection.Instance.Cartridge(obj.GameId).GameObject(obj.ObjectId),
			transform.position,
			transform.rotation
		);
		*/
	}
}