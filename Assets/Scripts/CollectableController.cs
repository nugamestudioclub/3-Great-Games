using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioSource))]
public class CollectableController : MonoBehaviour
{
    private Collider2D myCollider;
    [HideInInspector]
    public bool isCollected = false;
    private float collectedTime;

    private AudioSource audioSource;

    [SerializeField]
    AudioClip collectSound;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        myCollider = GetComponent<Collider2D>();
    }
    void Update()
    {
        if (isCollected)
        {

            collectedTime += Time.deltaTime;
            if (collectedTime > collectSound.length)
            {
                Destroy(gameObject);
            }
        }

    }
    //when touch the player 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            Collect();
    }
    protected virtual void Collect()
    {
        //play sound
        audioSource.PlayOneShot(collectSound);
        //
        myCollider.enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        isCollected = true;
    }
}
