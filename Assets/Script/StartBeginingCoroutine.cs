using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBeginingCoroutine : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] FirstsBlocks;
    public GameObject StartUI;
    public Animator animator;

    private void Start()
    {
        foreach (var block in FirstsBlocks)
        {
            block.GetComponent<Road>().enabled = false;
        }
        Player.GetComponent<Swipe>().enabled = false;
        animator = Player.GetComponent<Animator>();
    }

    public void HelpingFunction() { StartCoroutine(StartWait()); }

    IEnumerator StartWait()
    {
        StartUI.SetActive(false);
        animator.SetBool("IsSurprised", true);
        Debug.Log("PLZ WORK 1");
        yield return new WaitForSeconds(seconds: 2.5f);
        Debug.Log("PLZ WORK 2");
        foreach (var block in FirstsBlocks)
        {
            block.GetComponent<Road>().enabled = true;
        }
        Player.GetComponent<Swipe>().enabled = true;

    }
}
