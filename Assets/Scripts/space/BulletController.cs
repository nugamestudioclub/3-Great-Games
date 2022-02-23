using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [HideInInspector]
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float newY = this.transform.position.y + moveSpeed * Time.deltaTime;
        this.transform.position = new Vector2(this.transform.position.x, newY);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("GameObject1 collided with " + collision.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        } else if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
