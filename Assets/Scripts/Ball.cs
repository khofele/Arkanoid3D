using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float xMax = 8;
    [SerializeField] private float zMax = 8;
    [SerializeField] private Paddle paddle = null;

    private Vector3 spawnPosition = new Vector3(0, 0.5f, -6.5f);
    private Vector3 velocity = Vector3.zero;

    public Paddle Paddle
    {
        get => paddle;
        set
        {
            paddle = value;
        }
    }

    public Vector3 Veloctiy
    {
        get => velocity;
        set
        {
            velocity = value;
        }
    }

    private void Update()
    {
        if(GameManager.Instance.IsRunning == true)
        {
            transform.position += velocity * Time.deltaTime;
        }
        
        if(GameManager.Instance.IsRunning == false && Input.GetKeyDown(KeyCode.Space))
        {
            velocity = new Vector3(0, 0, zMax);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.transform.tag)
        {
            case "Wall":
                HitWall(other.gameObject.transform.tag);
                PlayHitSound();
                break;

            case "Ball":
                velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
                PlayHitSound();
                break;

            case "TopWall":
                HitWall(other.gameObject.transform.tag);
                PlayHitSound();
                break;

            case "DeadEnd":
                PlayHitSound();
                if (gameObject.CompareTag("Ball"))
                {
                    GameManager.Instance.Lives--;
                    if (GameManager.Instance.IsDead() == true)
                    {
                        gameObject.transform.position = spawnPosition;
                        velocity = Vector3.zero;
                        BoxManager.Instance.ResetBoxes();
                        ItemManager.Instance.ResetItems();
                    }
                    else
                    {
                        ResetBall();
                        paddle.ResetPaddle();
                    }
                } 
                else if(gameObject.CompareTag("AdditionalBall"))
                {
                    Destroy(gameObject);
                }

                break;

            case "Box":
                other.gameObject.GetComponent<Box>().HitCounter--;
                other.gameObject.GetComponent<Box>().CheckHitCounter();
                HitBox();
                PlayHitSound();
                ItemManager.Instance.Counter++;
                break;

            case "Paddle":
                HitPaddle(other);
                PlayHitSound();
                break;
        }
    }

    public void ResetBall()
    {
        gameObject.transform.position = spawnPosition;
        velocity = new Vector3(0, 0, zMax);
    }

    private void HitPaddle(Collider other)
    {
        float xmaxDistance = 0.5f * other.transform.localScale.x + 0.5f * transform.localScale.x;
        float dxistance = transform.position.x - other.transform.position.x;
        float xnormalizedDistance = dxistance / xmaxDistance;
        velocity = new Vector3(xnormalizedDistance * xMax, velocity.y, -velocity.z);
    }

    private void HitWall(string tag)
    {
        if(tag == "Wall")
        {
            velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
        } 
        else if (tag == "TopWall")
        {
            velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
        }
    }

    private void HitBox()
    {
        velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
    }

    private void PlayHitSound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
