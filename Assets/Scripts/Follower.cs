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

	public void Follow(Transform leader) {
		this.leader = leader;
		Transform();
	}

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
			freezePosition.x ? transform.position.x : leader.position.x + positionOffset.x,
			freezePosition.y ? transform.position.y : leader.position.y + positionOffset.y,
			freezePosition.z ? transform.position.z : leader.position.z + positionOffset.z
		);
	}

	private Vector3 NextRotation() {
		return new Vector3(
			freezeRotation.x ? transform.eulerAngles.x : leader.eulerAngles.x + rotationOffset.x,
			freezeRotation.y ? transform.eulerAngles.y : leader.eulerAngles.y + rotationOffset.y,
			freezeRotation.z ? transform.eulerAngles.z : leader.eulerAngles.z + rotationOffset.z
		);
	}
}