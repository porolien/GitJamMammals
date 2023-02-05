using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool WillBeRoot;
    private bool DontMakeTheRootFalse;
    public bool ItsRooted;
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

    public AudioSource source;
    public AudioClip[] SoundEffects = new AudioClip[3];

    public bool FirstTimeRooted = true;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
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
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary && !IsJumping)
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
                Debug.Log("EndRoot");
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
                                StartCoroutine(PlaySong());
                                source.PlayOneShot(SoundEffects[0]);
                                IsJumping = false;
                            }
                            else
                            {
                                source.PlayOneShot(SoundEffects[0]);
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

    IEnumerator PlaySong()
    {
        yield return new WaitForSeconds(0.15f);
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

    public void TakeDmg(int damage)
    {
        Hp = Hp - damage;
        if (Hp <= 0)
        {
            source.PlayOneShot(SoundEffects[1]);
            animator.SetBool("IsDead", true);
            AllBlock = GameObject.FindGameObjectsWithTag("Block");
            foreach (GameObject block in AllBlock)
            {
                block.GetComponent<Road>().speed = 0;
            }

        }
        else
        {
            source.PlayOneShot(SoundEffects[1]);
            animator.SetBool("IsHurt", true);
            StartCoroutine(SwitchHurtBool());
            StartCoroutine(Regen());
        }
    }
    IEnumerator Regen()
    {
        yield return new WaitForSeconds(2f);
        Hp = HpMax;
    }
    IEnumerator SwitchHurtBool()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("IsHurt", false);
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
                if (FirstTimeRooted)
                {
                    source.PlayOneShot(SoundEffects[2]);
                    FirstTimeRooted = false;

                }
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
                FirstTimeRooted = true;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Panthere" && other.GetComponent<CrossRoad>().DoStart)
        {
            if (other.tag == "Panthere" && other.GetComponent<CrossRoad>().DoStart)
            {
                source.PlayOneShot(other.GetComponent<CrossRoad>().Panthere[1]);
                TakeDmg(2);
            }
            if (other.tag == "Camion")
            {
                source.PlayOneShot(other.GetComponent<CrossRoad>().Camion[2]);
                TakeDmg(2);
            }
            if (other.tag == "Axe")
            {
                TakeDmg(2);
            }
        }
        else
        {
            Debug.Log("hello");
        }
    }
}
