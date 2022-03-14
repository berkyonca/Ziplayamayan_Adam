using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject EnemySpawn;
    private int i;
    private int enemyCount;
    private int waveNumber = 1;
    public GameObject boostUpStart;
    public AudioClip bellRing;
    TextMeshProUGUI WaveText;
    private void Start()
    {
        spawnEnemyWave(waveNumber);
        Invoke("ýnstantiateBoostUp", 4);
        WaveText = GameObject.Find("Canvas/WaveText").GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            GetComponent<AudioSource>().PlayOneShot(bellRing, 1f);
            StartCoroutine(PowerUpCountDownRoutine());
            spawnEnemyWave(waveNumber + 2);
            WaveText.text = "WAVE " + waveNumber.ToString();
        }
    }
    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(3);
        WaveText.gameObject.SetActive(true);
    }
    void ýnstantiateBoostUp()
    {
        Instantiate(boostUpStart, transform.position, transform.rotation);
    }
    void spawnEnemyWave(int enemySpawn)
    {
        for (i = 0; i < enemySpawn; i++)
        {
            Instantiate(EnemySpawn, GenerateSpawnPosition(), EnemySpawn.transform.rotation);
        }
    }
    private  Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-3, 7);
        float spawnPosZ = Random.Range(-7, 9);
        Vector3 randomPos = new Vector3(spawnPosX, 0.81f, spawnPosZ);
        return randomPos;
    }
    
}

