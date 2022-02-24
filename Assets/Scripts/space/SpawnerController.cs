using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField]
    private GameObject spawn;
    [SerializeField]
    private float spawnDelay;
    [SerializeField]
    private float waveDelay;
    [HideInInspector]
    private bool spawned;
    [HideInInspector]
    private bool inWave;
    // Start is called before the first frame update
    void Start()
    {
        spawned = false;
        inWave = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawned)
        {
            StartCoroutine(Spawn());
        }
        if (!inWave)
        {
            StartCoroutine(Wave());
        }
    }

    private IEnumerator Spawn()
    {
        spawned = true;
        Instantiate(spawn, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnDelay);
        spawned = false;
    }
    private IEnumerator Wave()
    {
        inWave = true;
        for (int i = -3; i <= 3; i++)
        {
            Vector3 pos = new Vector3(this.transform.position.x + i, this.transform.position.y, this.transform.position.z);
            Instantiate(spawn, pos, Quaternion.identity);
        }
        yield return new WaitForSeconds(waveDelay);
        inWave = false;
    }
}
