using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ShootController : MonoBehaviour {
	[SerializeField]
	private float moveSpeed;
	[SerializeField]
	private float shootDelay;
	[SerializeField]
	private GameObject projectile;
	[HideInInspector]
	private Action state;
	[HideInInspector]
	private int layerMask;
	[HideInInspector]
	private bool inShoot;
	[SerializeField]
	private float moveTime;
	[HideInInspector]
	private float originalTime;

	[SerializeField]
	private Entity entity;

	void Start() {
		state = Action.Move;
		layerMask = 1 << 3;
		inShoot = false;
		originalTime = moveTime;
	}

	void Update() {
		float newX = this.transform.position.x;
		float newY = this.transform.position.y;
		moveTime -= Time.deltaTime;
		if( state == Action.Move ) {
			newY -= moveSpeed * Time.deltaTime;
		}
		else if( state == Action.Shoot ) {
			if( !inShoot ) {
				StartCoroutine(Shoot());
			}
		}
		this.transform.position = new Vector2(newX, newY);
	}

	private void FixedUpdate() {
		if( moveTime <= 0 ) {
			state = Action.Shoot;
			moveTime = originalTime;
		}

	}

	void OnTriggerEnter2D(Collider2D collision) {
		if( collision.gameObject.CompareTag("Bullet") ) {
			GameMemory.Instance.ChanceOfCorruption(0.05);
			entity.Deactivate();
			Destroy(gameObject);
		}
	}

	private IEnumerator Shoot() {
		inShoot = true;
		Instantiate(projectile, this.transform.position, Quaternion.identity);
		state = Action.Move;
		yield return new WaitForSeconds(shootDelay);
		inShoot = false;
	}
	private enum Action {
		Move, Shoot
	}
}