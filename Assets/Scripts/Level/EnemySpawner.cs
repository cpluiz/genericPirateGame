using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour{
    [Header("Spawner Configurations")]
    [SerializeField, Range(3f, 10f)] float spawnInterval = 1f;
    [SerializeField, Range(0.5f, 2f)] float spawnVariation = 0.5f;
    [SerializeField] private BoatController[] enemyPrefabs;
    private Vector2 newPosition;
    public void StartSpawn(){
        spawnInterval = PlayerPrefs.GetFloat("spawnIntervalTime", 7f);
        StopAllCoroutines();
        StartCoroutine(nameof(SpawnEnemy));
    }
    private IEnumerator SpawnEnemy(){
        float nextSpawn = spawnInterval + Random.Range(-spawnVariation, spawnVariation);
        newPosition.y = Random.Range(0, Random.Range(CameraBounds.instance.top.y, CameraBounds.instance.top.y + 4f)) * Mathf.Sign(Random.Range(-2f, 2f));
        if(Mathf.Abs(newPosition.y) > CameraBounds.instance.top.y){
            newPosition.x = Random.Range(0, Random.Range(CameraBounds.instance.left.x, CameraBounds.instance.left.x - 4f)) * Mathf.Sign(Random.Range(-2f, 2f));
        }else{
            newPosition.x = (CameraBounds.instance.left.x - Random.Range(2f,4f)) * Mathf.Sign(Random.Range(-2f, 2f));
        }
        Instantiate<BoatController>(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], newPosition, Quaternion.identity);
        yield return new WaitForSeconds(nextSpawn);
        StartCoroutine(nameof(SpawnEnemy));
    }
}
