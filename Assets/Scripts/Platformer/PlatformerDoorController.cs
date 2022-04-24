using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformerDoorController : ColliderController
{
    [SerializeField]
    private Sprite openSprite;
    private SpriteRenderer sprite;
    [SerializeField]
    private CollectableController key;
    private bool keyCollected = false;
    private bool doorOpen = false;

    private AudioSource audioSource;

    [SerializeField]
    AudioClip collectSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }


    private void Update()
    {
        if (key.isCollected && !keyCollected)
        {
            keyCollected = true;
        }

        if (isColliding && keyCollected && !doorOpen)
        {
            sprite.sprite = openSprite;
            doorOpen = true;
            audioSource.PlayOneShot(collectSound);
        }

        if (isColliding && doorOpen && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            TransitionManager.ToPlatformerEnd();
        }
    }

    //if colliding with player -> open door

    //if colliding with player and door is open and player pressing up ->transistion to other area

}
