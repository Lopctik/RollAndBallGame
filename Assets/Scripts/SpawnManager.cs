using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    public GameObject boostSpeedPrefab;
    public GameObject boostMassPrefab;
    public GameObject boostSlowPrefab;
    public float spawnRange;
    public int waveCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveCount);    
    }

    // Update is called once per frame
    void Update()
    {
        int enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            waveCount++;
            ClearBoosters();
            SpawnEnemyWave(waveCount);
        }
    }

    private Vector3 GenerateSpawnPos()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        return new Vector3(spawnPosX, 0, spawnPosZ);
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
        }
        Instantiate(boostSpeedPrefab, GenerateSpawnPos(), boostSpeedPrefab.transform.rotation);
        Instantiate(boostSlowPrefab, GenerateSpawnPos(), boostSlowPrefab.transform.rotation);
        Instantiate(boostMassPrefab, GenerateSpawnPos(), boostMassPrefab.transform.rotation);
    }

    private void ClearBoosters()
    {
        Destroy(GameObject.FindGameObjectWithTag("MassBooster"));
        Destroy(GameObject.FindGameObjectWithTag("SpeedBooster"));
        Destroy(GameObject.FindGameObjectWithTag("SlowBooster"));
    }
}
