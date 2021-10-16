using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    private List<Box> allBoxes = new List<Box>();

    private static BoxManager instance = null;

    [SerializeField] private Material green = null;
    [SerializeField] private Material blue = null;
    [SerializeField] private Material yellow = null;
    [SerializeField] private Material red = null;

    public static BoxManager Instance
    {
        get => instance;
    }

    public List<Box> AllBoxes
    {
        get => allBoxes;
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

    void Start()
    {
        allBoxes.Clear();
        Box[] objects = GetComponentsInChildren<Box>();

        foreach(Box box in objects)
        {
            allBoxes.Add(box); 
        }
    }

    public void ResetBoxes()
    { 
        foreach (Box box in allBoxes)
        {
            box.gameObject.SetActive(true);
            box.HitCounter = box.PossibleHits;
            //box.gameObject.GetComponent<Renderer>().material.color = Color.white;
            //box.gameObject.GetComponent<Renderer>().material.color = box.DefaultColor;
            if(box.PossibleHits == 1)
            {
                box.gameObject.GetComponent<Renderer>().material = green;
            }
            else if(box.PossibleHits == 2)
            {
                box.gameObject.GetComponent<Renderer>().material = blue;
            }
            else if(box.PossibleHits == 3)
            {
                box.gameObject.GetComponent<Renderer>().material = yellow;
            }
            else if(box.PossibleHits == 4)
            {
                box.gameObject.GetComponent<Renderer>().material = red;
            }

        }
    }

    public bool CheckBoxes()
    {
        foreach(Box box in allBoxes)
        {
            if(box.gameObject.activeSelf == true)
            {
                return false;
            }
        }
        return true;
    }
}
