using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private int possibleHits;    // saves possible hits
    [SerializeField] private int hitCounter;
    [SerializeField] private int points;
    [SerializeField] private Color defaultColor;

    private List<Box> boxes = new List<Box>();
    private Renderer boxRenderer = null;

    private static Vector3 position = Vector3.zero;

    public int HitCounter
    {
        get => hitCounter;
        set
        {
            hitCounter = value;
        }
    }

    public int PossibleHits
    {
        get => possibleHits;
    }

    public Color DefaultColor
    {
        get => default;
    }

    public static Vector3 Position
    {
        get => position;
    }

    private void Start()
    {
        hitCounter = possibleHits;
        boxRenderer = gameObject.GetComponent<Renderer>();
        defaultColor = boxRenderer.material.GetColor("_Color");
    }

    public void CheckHitCounter()
    {
        if(hitCounter <= 0)
        {
            GameManager.Instance.Score += points;
            GameManager.Instance.CheckHighscore();
            position = gameObject.transform.position;
            gameObject.SetActive(false);
        }
        else
        {
            boxRenderer.material.color = boxRenderer.material.color * 0.7f;
        }
    }
}
