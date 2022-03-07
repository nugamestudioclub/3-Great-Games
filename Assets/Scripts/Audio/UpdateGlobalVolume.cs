using UnityEngine;

public class UpdateGlobalVolume : MonoBehaviour
{
    private AudioSource ac;

    void Awake()
    {
        //TODO: Subscribe to volume change event
        GlobalVolume.Instance.ProcessCompleted += UpdateVolume_ProcessCompleted;
        ac = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void UpdateVolume()
    {
        //TODO: change to on event trigger
        ac.volume = GlobalVolume.Instance.Volume;
    }


}
