using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private int counter = 0;
    private int random = 0;
    [SerializeField] private WidePaddleItem widePaddleItem = null;
    [SerializeField] private BallItem ballItem = null;
    [SerializeField] private Paddle paddle = null;

    private static ItemManager instance = null;
    private Item item = null;
    private List<Item> items = new List<Item>();

    public static ItemManager Instance
    {
        get => instance;
    }

    public int Counter
    {
        get => counter;
        set
        {
            counter = value;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        random = GetRandomNumber();
    }

    private void Update()
    {
        if (counter >= random)
        {
            counter = 0;
            int randomNumber = Random.Range(1, 11);
            if(randomNumber % 2 == 0)
            {
                item = Instantiate(widePaddleItem);
                InitializeWidePaddleItem(item, paddle, Box.Position);
                items.Add(item);
            } 
            else
            {
                item = Instantiate(ballItem);
                InitializeBallItem(item, paddle, Box.Position);
                items.Add(item);
            }
        }
    }

    private void InitializeWidePaddleItem(Item item, Paddle paddle, Vector3 spawnPos)
    {
        item.GetComponent<WidePaddleItem>().Paddle = paddle;
        item.transform.position = spawnPos;
        item.Velocity = new Vector3(0, 0, -5);
        item.Spawned = true;
        random = GetRandomNumber();
    }

    private void InitializeBallItem(Item item, Paddle paddle, Vector3 spawnPos)
    {
        item.GetComponent<BallItem>().Paddle = paddle;
        item.transform.position = spawnPos;
        item.Velocity = new Vector3(0, 0, -5);
        item.Spawned = true;
        random = GetRandomNumber();
    }

    private int GetRandomNumber()
    {
        int random = Random.Range(15, 25);
        return random;
    }

    public void ResetItems()
    {
        foreach(Item item in items)
        {
            if(item != null)
            {
                item.DeleteItem();
                Destroy(item);
            }
        }
    }
}
