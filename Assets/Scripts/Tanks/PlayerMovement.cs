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

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enable)
        {
            Vector3 mov;
            transform.Rotate(0, 0, Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime);
            Debug.Log(transform);
            mov = new Vector3(speed * Mathf.Cos(transform.rotation.z / 360 * 2 * Mathf.PI) * Input.GetAxis("Vertical") * Time.deltaTime, 
                speed * Mathf.Sin(transform.rotation.z / 360 * 2 * Mathf.PI) * Input.GetAxis("Vertical") * Time.deltaTime, 0);
            controller.Move(mov);

        }
    }
}
