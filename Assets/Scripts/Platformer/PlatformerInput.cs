using UnityEngine;

class PlatformerInput : MonoBehaviour, IGlitchyInput {
	public Vector2 Movement() {
		return new Vector2(
			Input.GetAxis("Horizontal"),
			Input.GetAxis("Vertical")
		);
	}

	public bool Jumping() {
		return Input.GetKeyDown(KeyCode.Space);
	}
}