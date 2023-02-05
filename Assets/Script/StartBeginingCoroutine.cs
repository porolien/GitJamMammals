using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBeginingCoroutine : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] FirstsBlocks;
    public GameObject StartUI;
    public Animator animator;

    public AudioSource source;
    public AudioClip[] Musics = new AudioClip[3];
    int Music_count = 0;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(Musics[0]);
        foreach (var block in FirstsBlocks)
        {
            block.GetComponent<Road>().enabled = false;
        }
        Player.GetComponent<Swipe>().enabled = false;
        animator = Player.GetComponent<Animator>();
    }

    private void Update()
    {
        // Play again the Menu music when it end if the player didn't starts to play

        if (Player.GetComponent<Swipe>().enabled == false && !source.isPlaying)
        {
            source.PlayOneShot(Musics[0]);
        }

        // Play the first game music twice 
        if (Music_count < 2 && !source.isPlaying)
        {
            source.PlayOneShot(Musics[1]);
        }

        // Plays the second game music after the first game music has been played twice 
        else if (!source.isPlaying) { source.PlayOneShot(Musics[2]); }
    }

    public void HelpingFunction()
    {
        source.Stop();
        source.PlayOneShot(Musics[1]);
        Music_count++;
        StartCoroutine(StartWait());
    }

    IEnumerator StartWait()  // NEVER PUT A "PUBLIC" BEFORE A COROUTINE
    {
        StartUI.SetActive(false);
        animator.SetBool("IsSurprised", true);
        yield return new WaitForSeconds(seconds: 2.5f);
        Player.GetComponent<Swipe>().enabled = true;
        foreach (var block in FirstsBlocks)
        {
            block.GetComponent<Road>().enabled = true;
        }

    }
}
