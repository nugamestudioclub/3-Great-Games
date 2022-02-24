using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Direction dir;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float newY = this.transform.position.y;
        if (dir == Direction.down)
        {
            newY -= moveSpeed * Time.deltaTime;
        }
        else if (dir == Direction.up)
        {
            newY += moveSpeed * Time.deltaTime;
        }
        this.transform.position = new Vector2(this.transform.position.x, newY);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (dir == Direction.down)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            else if (collision.gameObject.CompareTag("HorzWall"))
            {
                Destroy(gameObject);
            }
        }
        else if (dir == Direction.up)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            else if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("VertWall"))
            {
                Destroy(gameObject);
            }
        }
    }
    private enum Direction
    {
        down, up
    }
}
