using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    public AudioSource playerAudio;
    public ParticleSystem dirtParticle;
    public ParticleSystem particleExplosion;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioClip runningSound;
    public AudioClip crashObstacleSound;
    public float jumpForce = 620;
    public float gravityModifier = 2.5f;
    public bool isOnGround = true;
    public bool gameOver = false;
   
    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1.3f);
            dirtParticle.Stop();    
        }
         
        

    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {

            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            particleExplosion.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.8f);
            playerAudio.PlayOneShot(crashObstacleSound, 2f);
        }
        

    }
   
}
