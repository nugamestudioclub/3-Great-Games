using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BulletController : MonoBehaviour {
	private enum Direction {
		Down,
		Up,
	}

	[SerializeField]
	private float moveSpeed;

	[SerializeField]
	private Direction dir;

	[SerializeField]
	private Entity entity;

	void Update() {
		float newY = this.transform.position.y;
		if( dir == Direction.Down ) {
			newY -= moveSpeed * Time.deltaTime;
		}
		else if( dir == Direction.Up ) {
			newY += moveSpeed * Time.deltaTime;
		}
		this.transform.position = new Vector2(this.transform.position.x, newY);
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if( dir == Direction.Down && collision.gameObject.CompareTag("Player") ) {
			GameObject go = GameObject.FindWithTag("Audio");
			AudioManager other = (AudioManager)go.GetComponent(typeof(AudioManager));
			other.playSound();
			entity.Deactivate();
			Destroy(gameObject);
		}
		else if( dir == Direction.Up && collision.gameObject.CompareTag("Enemy") ) {
			GameObject go = GameObject.FindWithTag("Audio");
			AudioManager other = (AudioManager)go.GetComponent(typeof(AudioManager));
			other.playSound();
			entity.Deactivate();
			Destroy(gameObject);
		}
	}
}