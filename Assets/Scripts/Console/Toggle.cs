using System;
using UnityEngine;
public class Toggle : MonoBehaviour {
	[SerializeField]
	private SpriteRenderer upRenderer;

	[SerializeField]
	private SpriteRenderer downRenderer;

	[SerializeField]
	private Action onMouseDown;

	[SerializeField]
	private Action onMouseUp;

	[SerializeField]
	private AudioSource audioSource;

	[SerializeField]
	private AudioClip upSound;

	[SerializeField]
	private AudioClip downSound;

	void Awake() {
		upRenderer.enabled = true;
		downRenderer.enabled = false;
	}

	void OnMouseDown() {
		upRenderer.enabled = false;
		downRenderer.enabled = true;

		Play(downSound);
		Down();
	}

	void OnMouseUp() {
		upRenderer.enabled = true;
		downRenderer.enabled = false;

		Play(upSound);
		Up();
	}

	public virtual void Down() {}

	public virtual void Up() {}

	private void Play(AudioClip audioClip) {
		if( audioSource == null || audioClip == null )
			return;

		audioSource.PlayOneShot(audioClip);
	}
}