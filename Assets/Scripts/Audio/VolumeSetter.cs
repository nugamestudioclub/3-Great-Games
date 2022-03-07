using UnityEngine.UI;
using UnityEngine;

public class VolumeSetter : MonoBehaviour
{
    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat("VolumeLevel", 0.25f);
        slider.onValueChanged.AddListener(delegate {SetVolume(); });
    }


    void SetVolume()
    {
        PlayerPrefs.SetFloat("VolumeLevel", slider.value);
    }

}
