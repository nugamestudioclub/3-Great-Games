using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerTankMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotSpeed;

    [SerializeField]
    private bool enable;

    public UnityEvent OnShoot = new UnityEvent();

    private Rigidbody2D rigidbody2d;
    private Animator animator;
    private AudioSource audioSource;

    private bool moving;

    // Start is called before the first frame update
    private void Awake()
    {
        rigidbody2d = GetComponentInChildren<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rigidbody2d.velocity = Input.GetAxis("Vertical") * speed * Time.deltaTime * transform.up;
        transform.Rotate(0, 0, Input.GetAxis("Horizontal") * -rotSpeed * Time.deltaTime);

        if (Mathf.Abs(Input.GetAxis("Vertical")) > Mathf.Epsilon || Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Epsilon)
        {
            moving = true;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            moving = false;
            audioSource.Stop();
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("tank_driving") && moving)
        {
            animator.Play("tank_driving");
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            GameMemory.Instance.Corrupt();
            TransitionManager.ToMenu();
        }

    }


}
