using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiedsEnracinementAnimScript : MonoBehaviour
{
    public GameObject Player;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = Player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("IsRooted") /*&& Player.GetComponent<Swipe>().FirstTimeRooted*/)
        {
            gameObject.GetComponent<Animator>().SetBool("StartFootAnim", true);
        }
        else if (!anim.GetBool("IsRooted"))
        {
            gameObject.GetComponent<Animator>().SetBool("StartFootAnim", false);

        }
    }
}
