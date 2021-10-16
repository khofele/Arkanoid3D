using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidePaddleItem : Item
{
    public override void ActivateItem()
    {
        base.ActivateItem();
        if (Paddle.Wide == true)
        {
            base.DeleteItem();
            return;
        } 
        else
        {
            Paddle.Wide = true;
            Paddle.transform.localScale += new Vector3(3, 0, 0);
            timer = 200.0f;
            ItemRunning();
        }
    }


    public override void DeleteItem()
    {
        Paddle.Wide = false;
        if(Paddle.transform.localScale != new Vector3(3, 1, 1))
        {
            Paddle.transform.localScale -= new Vector3(3, 0, 0);
        }
        base.DeleteItem();
    }
}
