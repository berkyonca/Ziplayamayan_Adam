using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    float speed = 20.0f;
    public bool HasPowerup = false;
    public bool HasPowerup2 = false;
    private float powerUpStrength = 15.0f;
    public GameObject powerupIndicators;
    public GameObject powerUpPreFab;
    public AudioClip ballHit, boostHit;
    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }
    private void Update()
    { 
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);

        if (playerRb.transform.position.y <= -10) 
        {
            transform.position = new Vector3(0, .15f, 0);
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
        powerupIndicators.transform.position = transform.position + new Vector3(0, -.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("powerup"))
        {
            HasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDownRoutine());
            powerupIndicators.gameObject.SetActive(true);
            StartCoroutine(powerUpSpawner());
            GetComponent<AudioSource>().PlayOneShot(boostHit, 1f);
        }
    }

    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(5);
        HasPowerup = false;
        powerupIndicators.gameObject.SetActive(false);
    }



    IEnumerator powerUpSpawner()  // Bu fonksiyon powerUp 'ýn alýndýktan 10 saniye sonra tekrardan çýkmasý için yazýldý.
    {
        yield return new WaitForSeconds(10);
        Instantiate(powerUpPreFab, new Vector3(Random.Range(-8, 7), 0.38f, Random.Range(6, -8)), powerUpPreFab.transform.rotation);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && HasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<AudioSource>().PlayOneShot(ballHit, 1f);
        }
    }
}
