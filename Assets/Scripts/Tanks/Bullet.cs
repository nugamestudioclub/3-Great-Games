using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField]
    private Entity entity;

    public GameObject hitEffect;
    
    void OnCollisionEnter2D(Collision2D collision){
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        entity.Deactivate();
        Destroy(effect, 0.3f);
        Destroy(gameObject);
    }
}
