using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopAudio : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    [ReadOnly]
    private float time;

    [SerializeField]
    [ReadOnly]
    private float length;

    [SerializeField]
    private bool RandomStart;

    private static System.Random random = new System.Random();

    private void Awake()
    {

        audioSource = GetComponent<AudioSource>();

        length = audioSource.clip.length;
        StartCoroutine(LateStart(.25f));
    }

    IEnumerator LateStart(float delay)
    {
        yield return new WaitForSeconds(delay);
        Play();
    }

    private void Play()
    {
        time = RandomStart ? (float)random.NextDouble() * length : 0;
        audioSource.time = time;
        audioSource.Play();
    }


    private void Update()
    {

        if (time >= length)
        {
            Loop();
        }
        time += Time.deltaTime;
    }

    private void Loop()
    {
        time = 0;
        audioSource.Play();
    }
}
