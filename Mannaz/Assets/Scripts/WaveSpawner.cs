using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public Enemy enemyPreFab;

    public Transform SpawnLoc;

    public int numberOfEnemies = 3; 

    public float timeBetweenWaves = 2f;

    private float countdown = 3f;

    void Start()
    {
        StartCoroutine(FirstSpawn());
    }

    IEnumerator FirstSpawn()
    {
        yield return new WaitForSeconds(2f);
        InvokeRepeating("SpawnTroops", 0f, 8f);
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(.3f);
        }
    }
    void SpawnEnemy()
    {
        Enemy testEnemy = Instantiate(enemyPreFab, SpawnLoc.position, SpawnLoc.rotation);
    }

    void SpawnTroops()
    {
        StartCoroutine(SpawnWave());
    }
    }

