using System.Collections;
using UnityEngine;

public class SpaceController : MonoBehaviour {
	[ReadOnly]
	[SerializeField]
	private float xVelocity;

	[ReadOnly]
	[SerializeField]
	private float yVelocity;

	[SerializeField]
	private float acceleration;

	[SerializeField]
	private Object bullet;

	[SerializeField]
	private float shootDelay;

	private bool inShoot;

	[SerializeField]
	private AudioClip shootSound;

	private AudioSource audioSource;

	void Start() {
		xVelocity = 0;
		yVelocity = 0;
		inShoot = false;
		audioSource = GetComponentInChildren<AudioSource>();
	}

	void Update() {
		if( Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			xVelocity += acceleration * Time.deltaTime;
		}
		else if( Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			xVelocity -= acceleration * Time.deltaTime;
		}
		else if( Mathf.Abs(yVelocity) > Mathf.Epsilon ) {
			if( xVelocity < 0 ) {
				xVelocity += acceleration * Time.deltaTime;
			}
			else if( xVelocity > 0 ) {
				xVelocity -= acceleration * Time.deltaTime;
			}
		}

		if( Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			yVelocity -= acceleration * Time.deltaTime;
		}
		else if( Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			yVelocity += acceleration * Time.deltaTime;
		}
		else if( Mathf.Abs(yVelocity) > Mathf.Epsilon ) {
			if( yVelocity < 0 ) {
				yVelocity += acceleration * Time.deltaTime;
			}
			else if( yVelocity > 0 ) {
				yVelocity -= acceleration * Time.deltaTime;
			}
		}

		Vector2 delta = new Vector2(xVelocity, yVelocity) * Time.deltaTime;

		if( Mathf.Approximately(delta.x, 0) )
			delta.x = 0;
		if( Mathf.Approximately(yVelocity, 0) )
			delta.y = 0;

		transform.Translate(new Vector2(delta.x, delta.y));

		if( Input.GetKey(KeyCode.Space) && !inShoot ) {
			StartCoroutine(Shoot());
		}
	}

	IEnumerator Shoot() {
		inShoot = true;
		audioSource.PlayOneShot(shootSound);
		Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y + 0.15f, this.transform.position.z), Quaternion.identity);
		yield return new WaitForSeconds(shootDelay);
		inShoot = false;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		Vector2 vector = new Vector2(xVelocity, yVelocity);
		if (vector.magnitude > acceleration && !collision.gameObject.CompareTag("Moat"))
        {
			GameMemory.Instance.Corrupt();
			TransitionManager.ToMenu();
		}
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
		xVelocity = 0;
		yVelocity = 0;
	}

    void OnTriggerEnter2D(Collider2D other) {
		if( other.gameObject.CompareTag("Enemy") ) {
			GameMemory.Instance.Corrupt();
			TransitionManager.ToMenu();
		}
	}
}