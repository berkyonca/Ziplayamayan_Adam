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

    public float _jumpForce = 620;
    public float __gravityModifier = 2.5f;
    public bool _isOnGround = true;
    public bool gameOver = false;
   
    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= _gravityModifier;
        
    }
    public void Update()
    {

        PlayerJump();
         
        

    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1.3f);
            dirtParticle.Stop();    
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
           PlayerOnGround();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            PlayerHitObstacle();
        }
        

    }


    private void PlayerOnGround()
    {
         _isOnGround = true;
            dirtParticle.Play();
    }

    private void PlayerHitObstacle()
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
