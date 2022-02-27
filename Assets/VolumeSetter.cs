using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSetter : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("VolumeLevel", GetComponent<UnityEngine.UI.Slider>().value);
    }

}
