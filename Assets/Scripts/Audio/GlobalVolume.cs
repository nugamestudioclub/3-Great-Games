using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Notify();

public class GlobalVolume : MonoBehaviour
{
    public static GlobalVolume Instance { get; private set; }

    public event Notify ProcessCompleted;

    private float volume;

    public float Volume {
        get => volume;  
        set {
            volume = value;
            OnVolumeChanged();
        } 
    }

    protected virtual void OnVolumeChanged()
    {
        ProcessCompleted?.Invoke();
    }

     
    void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
    }
}
