using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave {
        public GameObject objectPrefab;
        public float spawnTimer;
        public float spawnInterval;
        public int objectsPerWave;
        public int spawnedObjectCount;
    }
    [SerializeField] private int _waveNumber;
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _minPos;
    [SerializeField] private Transform _maxPos;


    void Update()
    {
        Wave currentWave = GetCurrentWave();
        currentWave.spawnTimer += Time.deltaTime * Player.Instance.boost;
        if (currentWave.spawnTimer >= currentWave.spawnInterval)
        {
            currentWave.spawnTimer = 0f;
            SpawnObject();
        }
        if (currentWave.spawnedObjectCount >= currentWave.objectsPerWave)
        {
            _waveNumber++;
            if (_waveNumber >= _waves.Count)
                _waveNumber = 0;
        }
    }

    void SpawnObject()
    {
        Wave currentWave = GetCurrentWave();
        Instantiate(currentWave.objectPrefab, RandomSpawnPoint(), transform.rotation);
        currentWave.spawnedObjectCount++;
    }

    public Wave GetCurrentWave()
    {
        return _waves[_waveNumber];
    }

    private Vector2 RandomSpawnPoint()
    {
        Vector2 spawnPoint;

        spawnPoint.x = _minPos.position.x;
        spawnPoint.y = Random.Range(_minPos.position.y, _maxPos.position.y);

        return spawnPoint;
    }
}
