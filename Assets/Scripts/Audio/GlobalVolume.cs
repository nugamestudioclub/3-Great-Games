using System;
using UnityEngine;

public class GlobalVolume : MonoBehaviour
{
    public static GlobalVolume Instance { get; private set; }

    public event EventHandler<float> VolumeChanged;

    [ReadOnly]
    [SerializeField]
    private float volume;

    public float Volume
    {
        get => volume;
        set
        {
            volume = value;
            OnVolumeChanged();
        }
    }

    void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
        PlayerPrefs.SetFloat("VolumeLevel", 0.5f); //change to read properly
        Volume = PlayerPrefs.GetFloat("VolumeLevel", 0.25f);
    }


    protected virtual void OnVolumeChanged()
    {
        PlayerPrefs.SetFloat("VolumeLevel", Volume);
        VolumeChanged?.Invoke(this, Volume);
    }

    public void IncreaseVolume()
    {
        Volume += .02f;
        if (Volume > 1)
        {
            Volume = 1;
        }
    }

    public void DecreaseVolume()
    {
        Volume -= .02f;
        if (Volume < 0)
        {
            Volume = 0;
        }
    }

    public void SetVolume(float level)
    {
        Volume = Mathf.Clamp01(level);
    }
}
