using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public abstract class PlatformEnemyController : MonoBehaviour {
    public bool IsDying { get; private set; }
    public float moveSpeed;

    [SerializeField]
    private float corruptionChance = 0.2f;
    protected Rigidbody2D rb;
    private Animator animator;
    protected SpriteRenderer sr;
    protected AudioSource deathSound;

    private void Awake() {
        rb = GetComponentInChildren<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        deathSound = GetComponent<AudioSource>();
    }

    protected virtual void Update() {
        if( !IsDying ) {
            //do normal behavior
            Move();
        }
    }

    protected void Die() {
        GameMemory.Instance.ChanceOfCorruption(corruptionChance);

        //play sound
        deathSound.Play();
        //play animation
        animator.Play("ladybug_death");

        IsDying = true;
    }

    abstract protected void Move();
}
