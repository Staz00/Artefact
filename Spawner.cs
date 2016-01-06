using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public static Spawner spawner;

    public Transform[] spawnPoints;
    public Wave[] waves;
    public Enemy enemy;

    public int m_CurrentWaveNumber {
        get; private set;
    }
    private int m_RemainingEnemies;
    private int m_RemainingAlive;

    private float m_NextSpawnTime;

    private Wave m_CurrentWave;


    void Awake()
    {
        spawner = this;
    }

    void Start()
    {
        NextWave();
    }

    void Update()
    {
        if (m_RemainingEnemies > 0 && Time.time > m_NextSpawnTime)
        {
            m_RemainingEnemies--;
            m_NextSpawnTime = Time.time + m_CurrentWave.timeBetweenNextSpawn;

            //testing
            int randomIndex = Random.Range(0, spawnPoints.Length);

            Enemy newEnemy = Instantiate(enemy, spawnPoints[randomIndex].position + Vector3.up * .5f, Quaternion.identity) as Enemy;
            newEnemy.OnDeath += OnEnemyDeath;
        }
    }

    private void OnEnemyDeath()
    {
        m_RemainingAlive--;

        if(m_RemainingAlive == 0)
        {
            NextWave();
        }
    }

    private void NextWave()
    {
        m_CurrentWaveNumber++;

        if(m_CurrentWaveNumber -1 < waves.Length)
        {
            m_CurrentWave = waves[m_CurrentWaveNumber - 1];

            m_RemainingEnemies = m_CurrentWave.enemyCount;
            m_RemainingAlive = m_RemainingEnemies;
        }
    }

    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float timeBetweenNextSpawn;
    }
}
