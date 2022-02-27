using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceEnemySpawnerController : MonoBehaviour
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
    [HideInInspector]
    private bool firstSpawn;
    [HideInInspector]
    private bool firstWave;
    // Start is called before the first frame update
    void Start()
    {
        spawned = false;
        inWave = false;
        firstSpawn = true;
        firstWave = true;
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
        if (firstSpawn)
        {
            firstSpawn = false;
            yield return new WaitForSeconds(spawnDelay);
        } else
        {
            Instantiate(spawn, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
        spawned = false;
    }
    private IEnumerator Wave()
    {
        inWave = true;
        if (firstWave)
        {
            firstWave = false;
            yield return new WaitForSeconds(waveDelay);
        } else
        {
            for (int i = -1; i <= 1; i++)
            {
                Vector3 pos = new Vector3(this.transform.position.x + i, this.transform.position.y, this.transform.position.z);
                Instantiate(spawn, pos, Quaternion.identity);
            }
            yield return new WaitForSeconds(waveDelay);
        }
        inWave = false;
    }
}
