using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Direction dir;
    // Start is called before the first frame update


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
                GameObject go = GameObject.FindWithTag("Audio");
                AudioManager other = (AudioManager)go.GetComponent(typeof(AudioManager));
                other.playSound();
                Destroy(collision.gameObject);
                Destroy(gameObject);
                SceneManager.LoadScene("Menu_Scene");
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
                GameObject go = GameObject.FindWithTag("Audio");
                AudioManager other = (AudioManager)go.GetComponent(typeof(AudioManager));
                other.playSound();
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
