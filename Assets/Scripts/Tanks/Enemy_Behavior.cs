using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behavior : MonoBehaviour {
    [SerializeField]
    private GameObject me;

    [SerializeField]
    private float rotSpeed;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20;

    private float time_mark;

    private Animator ani;

    [SerializeField]
    private Entity entity;

    private bool isDying;
    void Awake() {

        time_mark = Time.frameCount;
        ani = GetComponent<Animator>();
    }
    private void Start()
    {
        PlayerPrefs.SetFloat("TankScore", PlayerPrefs.GetFloat("TankScore") + 1);
    }
    // Update is called once per frame
    void Update() {
        //transform.LookAt(player, new Vector3(0,0,1));
        //transform.rotation = new Quaternion(0, 0, transform.rotation.x * -1, 1);
        transform.Rotate(0, 0, -rotSpeed * Time.deltaTime);

        if( Time.frameCount - time_mark > 240 ) {
            Shoot();
            time_mark = Time.frameCount;
        }
    }

    private void Die()
    {
        isDying = true;
        GameMemory.Instance.Corrupt();

        PlayerPrefs.SetFloat("TankScore", PlayerPrefs.GetFloat("TankScore") - 1);
        if (PlayerPrefs.GetFloat("TankScore") <= 0)
        {
            TransitionManager.ToTankEnd();
        }
        entity.Deactivate();
        Destroy(me);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Colliding with: {collision.name}");
        if (collision.gameObject.CompareTag("Bullet") && !isDying)
        {
            Die();
        }
    }

    void Shoot() {
        GameMemory.Instance.ChanceOfCorruption(0.02);

        // Debug.Log("Shoot");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

    }
}
