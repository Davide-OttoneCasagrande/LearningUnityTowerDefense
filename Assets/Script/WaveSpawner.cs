using System.Collections;
using UnityEngine;
using Assets.Script;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    public Wave[] waves;
    int waveIndex = 0;
    public float timeBetweenWaves = 0.5f;
    float waveCountdown;
    public TMP_Text waveSpawnerUIText;
    float searchCountdown = 1f;
    SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
                WaveCompleted();
            else
                waveSpawnerUIText.text = "Figthing...";
            return;
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
                StartCoroutine(SpawnWave(waves[waveIndex]));
            else
                return;
        }
        else
        {
            waveCountdown -= Time.deltaTime;
            waveSpawnerUIText.text = "Enemy wave in: " + Mathf.Round(waveCountdown);
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed");
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        if (waveIndex < waves.Length - 1)
            waveIndex++;
        else
        {
            waveIndex = 0;
            Debug.Log("!!!ALL WAVES COMPLETED!!! looping");
        }

    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
                return false;
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave" + _wave.name);
        state = SpawnState.SPAWNING;
        waveSpawnerUIText.text = "Warning!!!\n" + _wave.name + " incoming.";
        for (int i = 0; i < _wave.amount; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }
        state = SpawnState.WAITING;
        yield break;
    }

    private void SpawnEnemy(Transform _enemy)
    {
        Vector3 spawnPosition = transform.position;
        //spawnPosition.y = 4.5f;
        Instantiate(_enemy, spawnPosition, transform.rotation);
    }
}