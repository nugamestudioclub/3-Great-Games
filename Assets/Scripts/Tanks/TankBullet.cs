using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : MonoBehaviour {
    [SerializeField]
    private Entity entity;

    public GameObject hitEffect;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = hitEffect.GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision){
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        entity.Deactivate();
        Destroy(effect, audioSource.clip.length);
        Destroy(gameObject);
    }
}
