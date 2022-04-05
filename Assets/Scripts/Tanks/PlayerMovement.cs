using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotSpeed;

    [SerializeField]
    private bool enable;

    [SerializeField]
    private TransitionManager tm;

    private Animator ani;

    public UnityEvent OnShoot = new UnityEvent();

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponentInChildren<Rigidbody2D>();

        ani = GetComponent<Animator>();

    }

    void Update(){
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

            rb2d.velocity = transform.up * Input.GetAxis("Vertical") * speed * Time.deltaTime;

            //transform.Translate(0, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0);
            transform.Rotate(0, 0, Input.GetAxis("Horizontal") * - rotSpeed * Time.deltaTime);

            if(!ani.GetCurrentAnimatorStateInfo(0).IsName("tank_driving") && Mathf.Abs(Input.GetAxis("Vertical")) > Mathf.Epsilon){
                ani.Play("tank_driving");
            }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            GameMemory.Instance.Corrupt();
            SceneManager.LoadScene("Menu_Scene");
        }
        
    }


}
