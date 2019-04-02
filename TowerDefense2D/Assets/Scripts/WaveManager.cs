using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Wave
{
    public int enemies;
    public float minSpawnRate, maxSpawnRate;
    public float delayToNext;
    public bool isRunning;
}

public class WaveManager : MonoBehaviour
{
    //public bool gameOver = false;
    public Wave[] waveSettings;
    public GameObject[] enemyPrefabs;
    public int waveIndex = 0;

    public Transform[] points;
    public int enemyCount = 0;

    #region Unity Events
    void Start()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        points = new Transform[children.Length - 1];
        for (int i = 1; i < children.Length; i++)
        {
            points[i - 1] = children[i];
        }

        StartNextWave();
    }

    IEnumerator StartWave(Wave currentWave)
    {
        while (enemyCount < currentWave.enemies)
        {
            enemyCount++;

            int randPoint = Random.Range(0, points.Length);
            Transform point = points[randPoint];

            // Spawn enemy
            int randEnemy = Random.Range(0, enemyPrefabs.Length);
            GameObject prefab = enemyPrefabs[randEnemy];
            Instantiate(prefab, point.position, point.rotation);

            float rate = Random.Range(currentWave.minSpawnRate, currentWave.maxSpawnRate);

            yield return new WaitForSeconds(rate);
        }

        // If no enemies are left
        yield return new WaitForSeconds(currentWave.delayToNext);

        enemyCount = 0;

        // Start new wave
        StartNextWave();

        print("Current Wave: " + (waveIndex + 1) + "");
    }

    void StartNextWave()
    {
        // Get current wave
        //needs an if waveIndex is not > waveSetting.Length
        if (waveIndex < waveSettings.Length)
        {
            Wave currentWave = waveSettings[waveIndex];
            // Run coroutine for spawning enemies on point
            StartCoroutine(StartWave(currentWave));
            // Increase index (for next wave)
            waveIndex++;
        }
        else
        {
            Debug.Log("ALL WAVES DONE");
        }
        
        
    }

    #endregion

}















//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[System.Serializable]
//public struct Wave
//{
//    public int enemies;
//    public float minSpawnRate, maxSpawnRate;
//}

//[System.Serializable]
//public struct Point
//{
//    public Transform point;
//    public float rate, timer;
//}

//public class WaveManager : MonoBehaviour
//{
//    public Wave[] waveSettings;
//    public GameObject[] enemyPrefabs;
//    public int waveIndex = 0;

//    private Point[] points;

//    [HideInInspector]
//    public int enemyCount = 0;

//    #region Unity Events
//    void Start()
//    {
//        GetAllPoints();
//    }
//    void Update()
//    {
//        if(enemyCount <= 0)
//        {
//            // Go to next wave!
//            waveIndex++; // There's a chance that the wave will skip a few because of Update
//        }

//        Wave currentWave = waveSettings[waveIndex];

//        for (int i = 0; i < points.Length; i++)
//        {
//            Point point = points[i];
//            float timer = point.timer;
//            float rate = point.rate;

//            timer += Time.deltaTime;
//            if (timer >= rate)
//            {
//                if (enemyCount < currentWave.enemies)
//                {
//                    SpawnEnemy();
//                    rate = Random.Range(currentWave.minSpawnRate, currentWave.maxSpawnRate);
//                    timer = 0f;
//                }
//            }

//            points[i].timer = timer;
//            points[i].rate = rate;
//        }
//    }
//    #endregion

//    #region Custom
//    void GetAllPoints()
//    {
//        Transform[] children = this.GetComponentsInChildren<Transform>();
//        points = new Point[children.Length - 1];
//        for (int i = 1; i < children.Length; i++)
//        {
//            points[i - 1].point = children[i]; 
//        }
//    }
//    void SpawnEnemy()
//    {
//        enemyCount++;

//        int randPoint = Random.Range(0, points.Length);
//        Transform point = points[randPoint].point;

//        int randEnemy = Random.Range(0, enemyPrefabs.Length);
//        GameObject prefab = enemyPrefabs[randEnemy];
//        GameObject clone = Instantiate(prefab, point.position, point.rotation);
//        EnemyStats enemy = clone.GetComponent<EnemyStats>();
//        enemy.owner = this; // I own the enemy!
//    }
//    #endregion
//}
