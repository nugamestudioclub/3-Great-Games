using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float shootDelay;
    [SerializeField]
    private GameObject projectile;
    [HideInInspector]
    private Action state;
    [HideInInspector]
    private int layerMask;
    [HideInInspector]
    private bool inShoot;
    // Start is called before the first frame update
    void Start()
    {
        state = Action.Move;
        layerMask = 1 << 3;
        inShoot = false;
    }

    // Update is called once per frame
    void Update()
    {
        float newX = this.transform.position.x;
        float newY = this.transform.position.y;
        if (state == Action.Move)
        {
            newY -= moveSpeed * Time.deltaTime;
        }
        else if (state == Action.Shoot)
        {
            if (!inShoot)
            {
                StartCoroutine(Shoot());
            }
        }
        this.transform.position = new Vector2(newX, newY);
    }

    private void FixedUpdate()
    {
        if (state == Action.Move)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 5, layerMask);
            if (hit.collider != null && hit.distance >= 3)
            {
                state = Action.Shoot;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private IEnumerator Shoot()
    {
        inShoot = true;
        Instantiate(projectile, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(shootDelay);
        inShoot = false;
    }
    private enum Action
    {
        Move, Shoot
    }
}
