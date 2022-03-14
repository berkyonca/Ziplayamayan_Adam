using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;
    private float speed = 3f;
    private void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        if(player.transform.position.y >= -0.3f)
        {
            Vector3 LookDirection = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(LookDirection * speed);
        }
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
