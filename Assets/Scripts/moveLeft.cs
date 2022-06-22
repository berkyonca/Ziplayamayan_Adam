using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveLeft : MonoBehaviour
{
    private float _speed = 25;
    private playerController playerControllerScript;
    private float _leftBoundry = -8f;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<playerController>();
    }

    
    void Update()
    {

       PlayerMovement();
       ObstacleStatus();
       

    }

    private void PlayerMovement()
    {
         if(!playerControllerScript.gameOver)

         { transform.Translate(Vector3.left * Time.deltaTime * _speed); }

    }

    private void ObstacleStatus()
    {
         if (transform.position.x < _leftBoundry && gameObject.CompareTag("Obstacle"))

        {
            Destroy(gameObject);
        }
    }






    
}
