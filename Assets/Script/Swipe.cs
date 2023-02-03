using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool WillBeRoot;
    private bool DontMakeTheRootFalse;
    private int RailPosition;
    public GameObject LeftPosition;
    public GameObject RightPosition;
    public GameObject MiddlePosition;
    public GameObject UpPosition;
    private Transform MyTransform;
    private bool IsSliding;
    private bool IsJumping;
    public int Hp;
    public int HpMax;
    // Start is called before the first frame update
    void Start()
    {
        MyTransform = GetComponent<Transform>();
        RailPosition = 2;
        Hp = 2;
        HpMax = Hp;
    }

    // Update is called once per frame
    void Update()
    {
        //We will be rooted if we don't move our finger
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            
            StartCoroutine(DelayForRoots());
        }
        else
        {
            DontMakeTheRootFalse = false;
        }
        //The Swipe Function
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
           
        }
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            //We Save our finger position to know where we want to move our player
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
           //In this situaition we swipe down
            if (makePositionXPositive < makePositionYPositive)
            {
                if (endTouchPosition.y < startTouchPosition.y)
                {
                    if (IsSliding)
                    {
                        Debug.Log("Nope, you can't Slide");
                    }
                    
                     else
                    {
                        if (IsJumping)
                        {
                            IsJumping = false;
                            MyTransform.position = new Vector3(MyTransform.transform.position.x, 0.51f, MyTransform.position.z);
                        }
                        else
                        {

                            IsSliding = true;
                            MyTransform.position = new Vector3(MyTransform.position.x, RightPosition.transform.position.y, MyTransform.position.z);
                            StartCoroutine(ChangingState());
                        }
                    }
                    Debug.Log("down");
                }
                //In this situaition we swipe up
                if (endTouchPosition.y > startTouchPosition.y)
                {
                    if (IsJumping)
                    {
                        Debug.Log("Nope, you can't Jump");
                    }
                    else 
                    {
                        if (IsSliding)
                        {
                            IsSliding = false;
                            MyTransform.position = new Vector3(MyTransform.transform.position.x, 0.51f, MyTransform.position.z);
                        }
                        else
                        {
                            IsJumping = true;
                            MyTransform.position = new Vector3(MyTransform.position.x, UpPosition.transform.position.y, MyTransform.position.z);
                            StartCoroutine(ChangingState());
                        }
                    }
                    Debug.Log("up");

                }
            }
            else 
            {
                //In this situaition we swipe left
                if (endTouchPosition.x < startTouchPosition.x)
                {
                    if (RailPosition == 1)
                    {
                        TakeDmg(1);
                    }
                    else
                    {
                        RailPosition--;
                        MoveThePlayer();
                    }
                    Debug.Log("left");
                }
                //In this situaition we swipe right
                if (endTouchPosition.x > startTouchPosition.x)
                {
                    if (RailPosition == 3)
                    {
                        TakeDmg(1);
                    }
                    else
                    {
                        RailPosition++;
                        MoveThePlayer();
                    }
                    Debug.Log("right");
                }
            }
            
        }
    }

    private void MoveThePlayer()
    {
        GameObject MyNewPosition = null;
        switch(RailPosition)
            {
            case 1:
                MyNewPosition = LeftPosition;
                break;
            case 2:
                MyNewPosition = MiddlePosition;
                break;
            case 3:
                MyNewPosition = RightPosition;
                break;

            }
        Debug.Log(MyNewPosition.transform.position.x);
        
        MyTransform.position = new Vector3(MyNewPosition.transform.position.x, MyTransform.position.y, MyTransform.position.z);
        Debug.Log(MyTransform.position.x);
    }

    private void TakeDmg(int damage)
    {
        Hp = Hp - damage;
        if(Hp <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(Regen());
        }
    }
    IEnumerator Regen()
    {
        yield return new WaitForSeconds(2f);
        Hp = HpMax;
    }
    IEnumerator ChangingState()
    {
        yield return new WaitForSeconds(1f);
        if (IsJumping)
        {
            IsJumping = false;
            MyTransform.position = new Vector3(MyTransform.transform.position.x, 0.51f, MyTransform.position.z);
        }
        else if (IsSliding)
        {
            IsSliding = false;
            MyTransform.position = new Vector3(MyTransform.transform.position.x, 0.51f, MyTransform.position.z);
        }
    }
    IEnumerator DelayForRoots()
    {
        if (!WillBeRoot)
        {


            WillBeRoot = true;
            DontMakeTheRootFalse = true;
            yield return new WaitForSeconds(0.07f);
            if (DontMakeTheRootFalse)
            {
                Debug.Log("Roots");
                DontMakeTheRootFalse = false;
                WillBeRoot = false;
            }
            else
            {
                WillBeRoot = false;
            }
        }
        
    }
}
