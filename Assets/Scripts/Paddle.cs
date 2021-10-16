using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private Transform field = null;
    [SerializeField] private Vector3 spawnPosition = new Vector3(0, 0.5f, -7.5f);
    private bool wide = false;

    public bool Wide
    {
        get => wide;
        set
        {
            wide = value;
        }
    }

    private void Update()
    {
        if(GameManager.Instance.IsRunning == true)
        {
            float dir = Input.GetAxis("Horizontal");
            float xMax = field.localScale.x * 10f * 0.5f - transform.localScale.x * 0.5f;   // die Hälfte der Breite des Fields - die Hälfte des Paddles
            float xPos = transform.position.x + speed * dir * Time.deltaTime;
            float clampedX = Mathf.Clamp(xPos, -xMax, xMax);

            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        } 
        else
        {
            ResetPaddle();
        }
    }

    public void ResetPaddle()
    {
        transform.position = spawnPosition;
        transform.localScale = new Vector3(3, 1, 1);
    }
}
