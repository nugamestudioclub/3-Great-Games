using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behavior : MonoBehaviour
{
    [SerializeField]
    private GameObject me;

    [SerializeField]
    private float rotSpeed;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20;

    private float time_mark;

    private Animator ani;

    void Start()
    {
        time_mark = Time.frameCount;
        ani = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(player, new Vector3(0,0,1));
        //transform.rotation = new Quaternion(0, 0, transform.rotation.x * -1, 1);
        transform.Rotate(0, 0, -rotSpeed * Time.deltaTime);

        if (Time.frameCount - time_mark > 240)
        {
            Shoot();
            time_mark = Time.frameCount;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( GameMemory.Instance.Rand.Next(10) == 0 )
            GameMemory.Instance.Corrupt();
        PlayerPrefs.SetFloat("TankScore", PlayerPrefs.GetFloat("TankScore") + 1);
        Destroy(me);
    }
    void Shoot()
    {
        Debug.Log("Shoot");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

    }
}
