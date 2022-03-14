using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spawnManager : MonoBehaviour
{
    public GameObject[ ] obstaclePrefab;
    private Vector3 spawnPos = new Vector3(32, 0, -0.86f);
    public float startDelay = 2;
    public float repeatRate = 2;
    private playerController playerControllerScript;
    private void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<playerController>();
        InvokeRepeating("SpawnObstacles", startDelay, repeatRate );
    }
    private void SpawnObstacles()
    {
       
        if(playerControllerScript.gameOver == false)
        {
            int randomObstacles = Random.Range(0, obstaclePrefab.Length);
            Instantiate(obstaclePrefab[randomObstacles], spawnPos, obstaclePrefab[randomObstacles].transform.rotation);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);

        }
    }
}
