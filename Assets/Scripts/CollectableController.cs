using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioSource))]
public class CollectableController : MonoBehaviour {
	private Collider2D myCollider;
	[HideInInspector]
	public bool isCollected = false;
	private float collectedTime;

	private AudioSource audioSource;

	private Entity entity;
	private SpriteRenderer spriteRenderer;

	[SerializeField]
	AudioClip collectSound;
	private void Awake() {
		audioSource = GetComponent<AudioSource>();
		myCollider = GetComponent<Collider2D>();
		entity = GetComponent<Entity>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}
	void Update() {
		if( isCollected ) {
			collectedTime += Time.deltaTime;
			if( collectedTime > collectSound.length ) {
				gameObject.SetActive(false);
				entity.Deactivate();
				//Destroy(gameObject);
			}
		}

	}
	//when touch the player 
	private void OnTriggerEnter2D(Collider2D collision) {
		if( collision.tag == "Player" )
			Collect();
	}
	protected virtual void Collect() {
		GameMemory.Instance.ChanceOfCorruption(0.33f);
		//play sound
		audioSource.PlayOneShot(collectSound);
		//
		myCollider.enabled = false;
		spriteRenderer.enabled = false;
		isCollected = true;
		entity.Deactivate();
	}
}
