using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotSpeed;

    [SerializeField]
    private bool enable;

    private CharacterController controller;

    private Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (enable)
        {

            transform.Translate(0, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0);
            transform.Rotate(0, 0, Input.GetAxis("Horizontal") * - rotSpeed * Time.deltaTime);

            if(!ani.GetCurrentAnimatorStateInfo(0).IsName("tank_driving") && Mathf.Abs(Input.GetAxis("Vertical")) > Mathf.Epsilon){
                ani.Play("tank_driving");
            }

        }


    }


}
