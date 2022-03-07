using System;
using UnityEngine;

public class GlobalVolume : MonoBehaviour
{
    public static GlobalVolume Instance { get; private set; }

    public event EventHandler<float> VolumeChanged;

    private float volume;

    public float Volume {
        get => volume;  
        set {
            volume = value;
            OnVolumeChanged();
        } 
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        } 
        else
        {
            DontDestroyOnLoad(this);
            Instance = this;
            Volume = PlayerPrefs.GetFloat("VolumeLevel", 0.25f);
        }
    }


    protected virtual void OnVolumeChanged()
    {
        PlayerPrefs.SetFloat("VolumeLevel", Volume);
        VolumeChanged?.Invoke(this, Volume); 
    }
}
