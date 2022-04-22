using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {
	[SerializeField]
	Transform subject;

	[SerializeField]
	private bool followPosition = true;

	[SerializeField]
	private Vector3 offsetPosition;

	[SerializeField]
	private bool followRotation = true;

	[SerializeField]
	private Vector3 offsetRotation;

	void Update() {
		if( followPosition )
			transform.position = subject.transform.position + offsetPosition;
		if( followRotation )
			transform.eulerAngles = subject.rotation.eulerAngles + offsetRotation;
	}
}