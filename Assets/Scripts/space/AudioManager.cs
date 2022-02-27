using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    private AudioClip explosion;
    [HideInInspector]
    private AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound()
    {
        audioSrc.PlayOneShot(explosion, 1f);
    }
}
