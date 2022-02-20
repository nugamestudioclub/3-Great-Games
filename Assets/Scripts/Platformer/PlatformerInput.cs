using UnityEngine;

class PlatformerInput : IGlitchyInput {
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