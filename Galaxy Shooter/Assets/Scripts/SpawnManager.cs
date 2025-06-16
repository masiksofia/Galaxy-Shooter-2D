using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _TripleShotPowerupPrefab;
    [SerializeField]
    private GameObject _SpeedPowerupPrefab;
    [SerializeField]
    private GameObject _ShieldsPrefab;


    private bool _IsStopSpawn = false;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerup());
        StartCoroutine(SpawnSpeedPowerup());
        StartCoroutine(SpawnShieldsPowerup());
    }


    //spawn gameobjects every 5 seconds
    private IEnumerator SpawnEnemy()
    {
        //yield return null; // wait 1 frame
        while (_IsStopSpawn == false)
        {
            Vector3 toSpawnEnemy = new Vector3(Random.Range(-8f, 8f), 7, 0);
            Instantiate(_enemyPrefab, toSpawnEnemy, Quaternion.identity);
            yield return new WaitForSeconds(3.0f);
        }
    }

    private IEnumerator SpawnPowerup()
    {
        while (_IsStopSpawn == false)
        {
            Vector3 toSpawnPowerup = new Vector3(Random.Range(-8f, 8f), 7, 0);
            Instantiate(_TripleShotPowerupPrefab, toSpawnPowerup, Quaternion.identity);
            yield return new WaitForSeconds(15.0f);
        }
    }

    private IEnumerator SpawnSpeedPowerup()
    {
        while(_IsStopSpawn == false )
        {
            Vector3 toSpeedSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            Instantiate(_SpeedPowerupPrefab, toSpeedSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(10.0f, 20.0f));
        }
    }

    private IEnumerator SpawnShieldsPowerup()
    {
        while(_IsStopSpawn == false )
        {
            Vector3 toShieldsSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            Instantiate(_ShieldsPrefab, toShieldsSpawn, Quaternion.identity);
            yield return new WaitForSeconds(15f);
        }
    }

    public void OnPlayerDeath()
    {
        _IsStopSpawn = true;
    }
}
