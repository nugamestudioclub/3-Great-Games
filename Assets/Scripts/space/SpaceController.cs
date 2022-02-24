using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : MonoBehaviour
{
    [HideInInspector]
    private float horzSpeed;
    [HideInInspector]
    private float vertSpeed;
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private Object bullet;
    [SerializeField]
    private float shootDelay;
    [SerializeField]
    private bool inShoot;
    // Start is called before the first frame update

    void Start()
    {
        horzSpeed = 0;
        vertSpeed = 0;
        inShoot = false;
    }

    // Update is called once per frame
    void Update()
    {


        float newX = this.transform.position.x;
        float newY = this.transform.position.y;
        if (Input.GetKey(KeyCode.D))
        {
            horzSpeed += acceleration * Time.deltaTime;
            newX += horzSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            horzSpeed -= acceleration * Time.deltaTime;
            newX += horzSpeed * Time.deltaTime;
        }
        else
        {
            if (horzSpeed < 0)
            {
                horzSpeed += acceleration * Time.deltaTime;
            }
            else if(horzSpeed > 0)
            {
                horzSpeed -= acceleration * Time.deltaTime;
            }
            newX += horzSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            vertSpeed -= acceleration * Time.deltaTime;
            newY += vertSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            vertSpeed += acceleration * Time.deltaTime;
            newY += vertSpeed * Time.deltaTime;
        }
        else
        {
            if (vertSpeed < 0)
            {
                vertSpeed += acceleration * Time.deltaTime;
            }
            else if (vertSpeed > 0)
            {
                vertSpeed -= acceleration * Time.deltaTime;
            }
            newY += vertSpeed * Time.deltaTime;
        }
        this.transform.position = new Vector2(newX, newY);

        if (Input.GetKey(KeyCode.Space) && !inShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        inShoot = true;
        Instantiate(bullet, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(shootDelay);
        inShoot = false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("HorzWall") || collision.gameObject.CompareTag("HorzWallTop"))
        {
            vertSpeed = 0;
        }
        else if (collision.gameObject.CompareTag("VertWall"))
        {
            horzSpeed = 0;
        }
    }

}
