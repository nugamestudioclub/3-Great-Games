using UnityEngine.UI;
using UnityEngine;

public class VolumeSetter : MonoBehaviour
{
    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate { SetVolume(); });
    }

    private void Start()
    {
        slider.value = GlobalVolume.Instance.Volume;
    }


    void SetVolume()
    {
        GlobalVolume.Instance.Volume = slider.value;
    }

}
