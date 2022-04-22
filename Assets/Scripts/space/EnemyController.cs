using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyController : MonoBehaviour {
	private enum Action {
		Right,
		Left,
		Down,
	}

	[SerializeField]
	private float moveSpeed;
	[HideInInspector]
	private Action state;
	[HideInInspector]
	private int layerMask;
	[HideInInspector]
	private bool down;

	[SerializeField]
	private Entity entity;

	void Start() {
		state = Action.Down;
		layerMask = 1 << 3;
		down = false;
	}

	void Update() {
		float newX = this.transform.position.x;
		float newY = this.transform.position.y;
		if( state == Action.Right ) {
			newX += moveSpeed * Time.deltaTime;
		}
		else if( state == Action.Down ) {
			newY -= moveSpeed * Time.deltaTime;
		}
		else if( state == Action.Left ) {
			newX -= moveSpeed * Time.deltaTime;
		}
		this.transform.position = new Vector2(newX, newY);
	}

	private void FixedUpdate() {

		if( state == Action.Right ) {
			RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, .2f, layerMask);
			if( hit.collider != null ) {
				state = Action.Down;
			}
		}
		else if( state == Action.Left ) {
			RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, .2f, layerMask);
			if( hit.collider != null ) {
				state = Action.Down;
			}
		}
		else if( state == Action.Down && !down ) {
			StartCoroutine(Delay());
		}
	}

	private IEnumerator Delay() {
		down = true;
		yield return new WaitForSeconds(0.7f);
		down = false;
		RaycastHit2D hit1 = Physics2D.Raycast(transform.position, Vector2.right, 6, layerMask);
		RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.left, 6, layerMask);
		if( hit1.collider == null || hit2.collider == null ) {

		}
		else if( Mathf.Abs(hit1.distance) - Mathf.Abs(hit2.distance) >= 0 ) {
			state = Action.Right;
		}
		else {
			state = Action.Left;
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if( collision.gameObject.CompareTag("Bullet") ) {
			entity.Deactivate();
			Destroy(gameObject);
		}
	}
}
