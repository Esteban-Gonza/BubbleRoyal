using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
    [SerializeField] GameObject spikePrefab;
    [SerializeField] int poolSize = 20;
    [SerializeField] float spawnInterval = 0.5f;
    [SerializeField] float minX = -7f;
    [SerializeField] float maxX = 7f;
    [SerializeField] float minSpawnDelay = 0.1f;
    [SerializeField] float maxSpawnDelay = 0.3f;

    private List<GameObject> spikePool;
    private float timer;

    private void Start()
    {
        StartSuddenDead();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            StartCoroutine(SpawnSpikeRainWithGap());
            timer = 0f;
        }
    }

    IEnumerator SpawnSpikeRainWithGap()
    {
        int gapIndex = Random.Range(0, poolSize);

        for (int i = 0; i < poolSize; i++)
        {
            if (i == gapIndex) continue;

            GameObject spike = spikePool[i];
            if (spike != null && !spike.activeInHierarchy)
            {
                float randomX = Random.Range(minX, maxX);
                spike.transform.position = new Vector3(randomX, 6f, 0f);
                spike.SetActive(true);

                float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
                yield return new WaitForSeconds(delay);
            }
        }
    }

    public void StartSuddenDead()
    {
        spikePool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject spike = Instantiate(spikePrefab);
            spike.SetActive(false);
            spikePool.Add(spike);
        }
    }

    public void RecycleSpike(GameObject spike, Rigidbody2D playerRigid)
    {
        float randomX = Random.Range(minX, maxX);
        spike.transform.position = new Vector3(randomX, 6f, 0f);
        spike.SetActive(true);
        if(playerRigid.gravityScale >= 20)
        {
            playerRigid.gravityScale = 20;
        }
        else
        {
            playerRigid.gravityScale *= 1.2f;
        }
    }
}