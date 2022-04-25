using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 3;

    private AudioSource audioSource;

    public float fireRate = 1.5f;

    private bool firing;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !firing)
        {
            StartCoroutine(Fire());
        }
    }

    IEnumerator Fire()
    {
        firing = true;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        audioSource.Play();
        yield return new WaitForSeconds(fireRate);
        firing = false;
    }
}
