using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    public Vector3 startposition;
    public Vector3 endposition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BeginDragging()
    {
        startposition= Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public void EndDragging()
    {
        
        endposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (AllBoxesLvls.isFree)
        {
            if (Math.Abs(endposition.x - startposition.x) > Math.Abs(endposition.y - startposition.y))
            {
                if (endposition.x < startposition.x)
                    AllBoxesLvls.MoveBoxes("left");
                else if (endposition.x > startposition.x)
                    AllBoxesLvls.MoveBoxes("right");

            }
            else
            {

                if (endposition.y < startposition.y)
                    AllBoxesLvls.MoveBoxes("down");
                else if (endposition.y > startposition.y)
                    AllBoxesLvls.MoveBoxes("up");
            }
        }
    }
}
