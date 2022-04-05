using UnityEngine;

public class UpdateGlobalVolume : MonoBehaviour
{
    private AudioSource ac;

    void Awake()
    {
        ac = GetComponent<AudioSource>();
    }

    private void Start()
    {
        //Subscribe to volume change event
        ac.volume = GlobalVolume.Instance.Volume;
        GlobalVolume.Instance.VolumeChanged += GlobalVolume_Changed;
    }

    void GlobalVolume_Changed(object sender, float e) 
    {
        //on event trigger
        if (ac != null)
        {
            ac.volume = e;

        }
        
    }


}
