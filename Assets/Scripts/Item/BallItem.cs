using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallItem : Item
{
    [SerializeField] private Ball ballPrefab = null;
    private List<Ball> balls = new List<Ball>();
    private Vector3 ballSpawnPos = new Vector3(0, 0.5f, -6.5f);

    public override void ActivateItem()
    {
        base.ActivateItem();
        timer = 500.0f;
        StartCoroutine(InstantiateBalls());
        ItemRunning();
    }

    public override void DeleteItem()
    {
        for(int i = 0; i < balls.Count; i++)
        {
            if(balls[i] != null)
            {
                Destroy(balls[i].gameObject);
            }
        }
        balls.Clear();
        base.DeleteItem();
    }

    private IEnumerator InstantiateBalls()
    {
        for (int i = 0; i < 2; i++)
        {
            Ball ball = Instantiate(ballPrefab);
            InitializeBall(ball, Paddle, ballSpawnPos);
            balls.Add(ball);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void InitializeBall(Ball ball, Paddle paddle, Vector3 ballPos)
    {
        ball.GetComponent<Ball>().Paddle = paddle;
        ball.transform.position = ballPos;
        ball.Veloctiy = new Vector3(0, 0, 8);
    }
}
