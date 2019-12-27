using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public Guards enemyGuard;
    public Wizard enemyWizard;
    public Bowman enemyBowman;

    public Transform SpawnLoc;

    private int numberOfEnemies = 1; 

    public float timeBetweenWaves = 2f;

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
            yield return new WaitForSeconds(1f);
        }
    }
    void SpawnEnemy()
    {
        if (transform.name == "Guard Tower")
        {
            Guards testEnemy = Instantiate(enemyGuard, SpawnLoc.position, SpawnLoc.rotation);
        }
        else if (transform.name == "Bowman Tower")
        {
            Bowman testEnemy = Instantiate(enemyBowman, SpawnLoc.position, SpawnLoc.rotation);
        }
        else if (transform.name == "Wizard Tower")
        {
            Wizard testEnemy = Instantiate(enemyWizard, SpawnLoc.position, SpawnLoc.rotation);
        }
    }

    void SpawnTroops()
    {
        StartCoroutine(SpawnWave());
    }
    }

