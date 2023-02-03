using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool WillBeRoot;
    private bool DontMakeTheRootFalse;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            
            StartCoroutine(DelayForRoots());
        }
        else
        {
            DontMakeTheRootFalse = true;
        }
        
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
           
        }
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {

            endTouchPosition = Input.GetTouch(0).position;
            float makePositionXPositive = endTouchPosition.x - startTouchPosition.x;
            float makePositionYPositive = endTouchPosition.y - startTouchPosition.y;
            if (endTouchPosition.x < startTouchPosition.x)
            {
                makePositionXPositive = -makePositionXPositive;
            }
            if (endTouchPosition.y < startTouchPosition.y)
            {
                makePositionYPositive = -makePositionYPositive ;
            }
           
            if (makePositionXPositive < makePositionYPositive)
            {
                if (endTouchPosition.y < startTouchPosition.y)
                {
                    Debug.Log("down");
                }
                if (endTouchPosition.y > startTouchPosition.y)
                {
                    Debug.Log("up");
                }
            }
            else 
            {
                
                if (endTouchPosition.x < startTouchPosition.x)
                {
                    Debug.Log("left");
                }
                if (endTouchPosition.x > startTouchPosition.x)
                {
                    Debug.Log("right");
                }
            }
            
        }
    }

    IEnumerator DelayForRoots()
    {
        WillBeRoot = true;
        yield return new WaitForSeconds(0.07f);
        if (DontMakeTheRootFalse)
        {
            DontMakeTheRootFalse = false;
        }
        else
        {
            Debug.Log("Roots");
        }
        
    }
}
