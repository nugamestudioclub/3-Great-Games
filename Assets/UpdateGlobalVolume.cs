using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGlobalVolume : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("VolumeLevel");
    }
}
