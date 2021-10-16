using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Item : MonoBehaviour
{
    protected bool active = false;
    protected bool spawned = false;
    protected float timer = 0.0f;
    protected Vector3 velocity = Vector3.zero;
    [SerializeField] private Paddle paddle = null;

    public Paddle Paddle
    {
        get => paddle;
        set
        {
            paddle = value;
        }
    }

    public bool Spawned
    {
        get => spawned;
        set
        {
            spawned = value;
        }
    }

    public Vector3 Velocity
    {
        get => velocity;
        set
        {
            velocity = value;
        }
    }

    private void FixedUpdate()
    {
        if (spawned == true)
        {
            transform.position += velocity * Time.deltaTime;
        }

        if (ItemRunning() == false && active == true)
        {
            active = false;
            DeleteItem();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.transform.tag)
        {
            case "Paddle":
                ActivateItem();
                break;

            case "DeadEnd":
                Destroy(gameObject);
                break;
        }
    }

    public virtual void ActivateItem()
    {
        active = true;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public virtual void DeleteItem()
    {
        if(gameObject != null)
        {
            Destroy(gameObject);
        }
    }

    public bool ItemRunning()
    {
        while (timer >= 0)
        {
            timer--;
            return true;
        }
        return false;
    }
}
