using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool WillBeRoot;
    private bool DontMakeTheRootFalse;
    private bool ItsRooted;
    private int RailPosition;
    public GameObject LeftPosition;
    public GameObject RightPosition;
    public GameObject MiddlePosition;
    public GameObject UpPosition;
    private Transform MyTransform;
    private bool IsSliding;
    private bool IsJumping;
    private Animator animator;
    public int Hp;
    public int HpMax;
    public float yPos;
    public Canvas GameOverScreen;
    public Road TheRoad;

    public GameObject[] AllBlock;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;

        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            if (ItsRooted)
            {
                ItsRooted = false;
                AllBlock = GameObject.FindGameObjectsWithTag("Block");
                foreach (GameObject block in AllBlock)
                {
                    block.GetComponent<Road>().speed = 1;
                }
                animator.SetBool("IsRooted", false);

            }
            else
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
                    makePositionYPositive = -makePositionYPositive;
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
                            animator.SetBool("IsSliding", true);

                            if (IsJumping)
                            {
                                IsJumping = false;
                            }
                            else
                            {
                                IsSliding = true;
                                StartCoroutine(ChangingState());
                            }
                            StartCoroutine(SlideWait());
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
                            animator.SetBool("IsJumping", true);

                            if (IsSliding)
                            {
                                IsSliding = false;
                            }
                            else
                            {
                                IsJumping = true;
                                StartCoroutine(ChangingState());
                            }
                            StartCoroutine(JumpWait());
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
    }

    private void MoveThePlayer()
    {
        GameObject MyNewPosition = null;
        switch (RailPosition)
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
        if (Hp <= 0)
        {
            GameOverScreen.gameObject.SetActive(true);
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
        }
        else if (IsSliding)
        {
            IsSliding = false;
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
                AllBlock = GameObject.FindGameObjectsWithTag("Block");
                foreach (GameObject block in AllBlock)
                {
                    block.GetComponent<Road>().speed = 0;
                }
                Debug.Log("Roots");
                animator.SetBool("IsRooted", true);
                DontMakeTheRootFalse = false;
                WillBeRoot = false;
                ItsRooted = true;
            }
            else
            {
                WillBeRoot = false;
            }
        }

    }
    IEnumerator SlideWait()
    {
            Debug.Log("Slidewit");
            yield return new WaitForSeconds(0.1f);
            animator.SetBool("IsSliding", false);
    }

    IEnumerator JumpWait()
    {
        Debug.Log("jumpwait");
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("IsJumping", false);

    }
}
