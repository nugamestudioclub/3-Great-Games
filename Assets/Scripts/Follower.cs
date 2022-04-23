using System;
using UnityEngine;

public class Follower : MonoBehaviour {
	[SerializeField]
	Transform leader;

	[SerializeField]
	private GenericVector3<bool> freezePosition;

	[SerializeField]
	private Vector3 positionOffset;

	[SerializeField]
	private GenericVector3<bool> freezeRotation;

	[SerializeField]
	private Vector3 rotationOffset;

	void Update() {
		Transform();
	}

	public void Follow(Transform leader) => this.leader = leader;

	public void FreezePosition(bool x, bool y, bool z) {
		freezePosition = new GenericVector3<bool>(x, y, z);
	}

	public void FreezeRotation(bool x, bool y, bool z) {
		freezeRotation = new GenericVector3<bool>(x, y, z);
	}

	private void Transform() {
		if( leader == null )
			return;

		transform.position = NextPosition();
		transform.eulerAngles = NextRotation();
	}

	private Vector3 NextPosition() {
		return new Vector3(
			leader.position.x + (freezePosition.x ? 0 : positionOffset.x),
			leader.position.y + (freezePosition.y ? 0 : positionOffset.y),
			leader.position.z + (freezePosition.z ? 0 : positionOffset.z)
		);
	}

	private Vector3 NextRotation() {
		return new Vector3(
			leader.eulerAngles.x + (freezeRotation.x ? 0 : rotationOffset.x),
			leader.eulerAngles.y + (freezeRotation.y ? 0 : rotationOffset.y),
			leader.eulerAngles.z + (freezeRotation.z ? 0 : rotationOffset.z)
		);
	}
}